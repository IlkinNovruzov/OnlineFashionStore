using Microsoft.AspNetCore.Mvc;
using OnlineFashionStore.Areas.Admin.Models.ViewModels;
using OnlineFashionStore.Models;

namespace OnlineFashionStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;
        public BrandController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult GetBrands()
        {
            var model = new BrandVM()
            {
                Brands = _context.Brands.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(BrandVM model)
        {
            await _context.Brands.AddAsync(model.Brand);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetBrands");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BrandVM model)
        {
            _context.Brands.Update(model.Brand);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetBrands");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var brand = _context.Brands.FirstOrDefault(x => x.Id == id);
            if (brand == null)
            {
                return RedirectToAction("GetBrands");
            }
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetBrands");
        }
       
        
    }
}
