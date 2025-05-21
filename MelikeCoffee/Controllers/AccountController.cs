using Microsoft.AspNetCore.Mvc;
using MelikeCoffee.Data;
using MelikeCoffee.Models;
using System.Linq;

public class AccountController : Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(User user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        bool emailExists = _context.Users.Any(u => u.Email == user.Email);
        if (emailExists)
        {
            ViewBag.Error = "This email is already registered.";
            return View(user);
        }

        _context.Users.Add(user);
        _context.SaveChanges();

        return RedirectToAction("SignIn");
    }

    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SignIn(LoginViewModel loginUser)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Error = "Please fill out the required fields.";
            return View(loginUser);
        }

        var user = _context.Users
            .FirstOrDefault(u => u.Email == loginUser.Email && u.Password == loginUser.Password);

        if (user == null)
        {
            ViewBag.Error = "Incorrect email or password.";
            return View(loginUser);
        }

        HttpContext.Session.SetString("UserName", user.FirstName);
        HttpContext.Session.SetString("Email", user.Email);
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Profile()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ChangePassword(string CurrentPassword, string NewPassword, string ConfirmPassword)
    {
        var email = HttpContext.Session.GetString("Email");
        if (email == null)
        {
            TempData["Error"] = "Please login first.";
            return RedirectToAction("SignIn");
        }

        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null)
        {
            TempData["Error"] = "User not found";
            return RedirectToAction("Profile");
        }

        if (user.Password != CurrentPassword)
        {
            TempData["Error"] = "Current password is incorrect.";
            return RedirectToAction("Profile");
        }

        if (NewPassword != ConfirmPassword)
        {
            TempData["Error"] = "New passwords do not match.";
            return RedirectToAction("Profile");
        }

        user.Password = NewPassword;
        _context.SaveChanges();

        TempData["Success"] = "Password changed successfully.";
        return RedirectToAction("Profile");
    }

    [HttpPost]
    public IActionResult DeleteAccount()
    {
        var email = HttpContext.Session.GetString("Email");
        if (email == null) return RedirectToAction("SignIn");

        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
            HttpContext.Session.Clear();
        }

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Orders()
    {
        var email = HttpContext.Session.GetString("Email");
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null) return RedirectToAction("SignIn");

        var orders = _context.OrderItems
            .Where(o => o.UserId == user.UserId)
            .ToList();

        return View(orders);
    }

    [HttpPost]
    public IActionResult CancelOrder(int orderItemId)
    {
        var orderItem = _context.OrderItems.FirstOrDefault(o => o.OrderItemId == orderItemId);
        if (orderItem != null)
        {
            _context.OrderItems.Remove(orderItem);
            _context.SaveChanges();
        }
        return RedirectToAction("Orders");
    }

}
