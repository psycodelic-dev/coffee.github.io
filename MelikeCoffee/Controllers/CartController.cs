using Microsoft.AspNetCore.Mvc;
using MelikeCoffee.Data;
using MelikeCoffee.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

public class CartController : Controller
{
    private readonly AppDbContext _context;

    public CartController(AppDbContext context)
    {
        _context = context;
    }


    [HttpPost]
    public IActionResult AddToCart(int ProductId, string ProductName, string Size, decimal Price)
    {
        var userName = HttpContext.Session.GetString("UserName");
        
        //Leads you to Index page, if you haven't signed up.
        if (string.IsNullOrEmpty(userName))
        {
            TempData["CartError"] = "Please login to add to cart.";
            return RedirectToAction("Index", "Categories");
        }

        var userEmail = HttpContext.Session.GetString("Email");
        var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);

        if (user == null) return RedirectToAction("SignIn", "Account");

        var cartItem = new CartItem
        {
            ProductId = ProductId,
            ProductName = ProductName,
            Size = Size,
            Price = Price,
            UserId = user.UserId
        };

        _context.CartItems.Add(cartItem);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Index()
    {
        var email = HttpContext.Session.GetString("Email");
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null) return RedirectToAction("SignIn", "Account");

        var cartItems = _context.CartItems
            .Where(ci => ci.UserId == user.UserId)
            .ToList();

        ViewBag.TotalPrice = cartItems.Sum(ci => ci.Price);

        return View(cartItems);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var item = _context.CartItems.Find(id);
        if (item != null)
        {
            _context.CartItems.Remove(item);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult PlaceOrder()
    {
        var email = HttpContext.Session.GetString("Email");
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null) return RedirectToAction("SignIn", "Account");

        var cartItems = _context.CartItems.Where(c => c.UserId == user.UserId).ToList();

        foreach (var item in cartItems)
        {
            var order = new OrderItem
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Size = item.Size,
                Price = item.Price,
                UserId = user.UserId
            };
            _context.OrderItems.Add(order);
        }

        _context.CartItems.RemoveRange(cartItems);
        _context.SaveChanges();

        return RedirectToAction("Orders", "Account");
    }


}
