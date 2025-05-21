using Microsoft.AspNetCore.Mvc;
using MelikeCoffee.Data;
using System.Linq;

public class CategoriesController : Controller
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var categories = _context.Categories.ToList();
        return View(categories);
    }

    public IActionResult ProductsByCategory(string category)
    {
        var products = _context.Products
            .Where(p => p.Category == category)
            .ToList();

        ViewBag.Category = category;
        return View(products);
    }
}
