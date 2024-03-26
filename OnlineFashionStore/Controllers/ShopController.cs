using OnlineFashionStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using OnlineFashionStore.Models.ViewModels;

namespace OnlineFashionStore.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetProducts()
        {
            var model = new ShopViewModel()
            {
                Products = await _context.Products.Include(p => p.Images).Include(x => x.Category).Where(x => x.IsActive == true).ToListAsync(),
                Categories = _context.Categories.ToList(),
                Brands = _context.Brands.ToList(),
                Colors = _context.Colors.ToList(),
                BrandProductCounts = _context.Products.GroupBy(p => p.Brand.Name).Select(g => new BrandProductCount{BrandName = g.Key,ProductCount = g.Count()}).ToList()
            };
            return View(model);
        }
        public async Task<IActionResult> ProductDetails(int id)
        {
            var product =await _context.Products.Include(p=>p.Images).Include(p => p.ProductColors).ThenInclude(pc => pc.Color).FirstOrDefaultAsync(p => p.Id == id);
            return View(product);
        }
    }
}
