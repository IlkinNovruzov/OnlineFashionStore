using OnlineFashionStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OnlineFashionStore.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult GetProducts()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View(_context.Products.Include(x => x.Category).ToList());
        }
    }
}
