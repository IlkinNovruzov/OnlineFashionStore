using OnlineFashionStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFashionStore.Models.ViewModels;

namespace OnlineFashionStore.Controllers
{
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
                BrandProductCounts = _context.Products.GroupBy(p => p.Brand.Name).Select(g => new BrandProductCount { BrandName = g.Key, ProductCount = g.Count() }).ToList()
            };
            return View(model);
        }
        public async Task<IActionResult> ProductDetails(int id)
        {
            var model = new ProductDetailsVM()
            {
                Product = await _context.Products.Include(p => p.Images).Include(a => a.Attributes).Include(p => p.ProductColors).ThenInclude(pc => pc.Color).FirstOrDefaultAsync(p => p.Id == id),
                Reviews = _context.Reviews.Where(r => r.ProductId == 1).Include(r => r.User).ToList()
            };
            return View(model);
        }

        public IActionResult ShopCart()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Review(ReviewViewModel model)
        {

            return View();
        }
    }

}
