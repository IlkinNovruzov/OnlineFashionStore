using Microsoft.AspNetCore.Mvc;
using OnlineFashionStore.Models;
using OnlineFashionStore.Models.DataModels;

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
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult BrandList()
        {
            return Json(_context.Brands.ToList());
        }
        [HttpPost]
        public JsonResult Add(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
            return Json("Added.");
        }
        [HttpGet]
        public JsonResult Edit(int id)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.Id == id);
            return Json(brand);
        }
        [HttpPost]
        public JsonResult Update(Brand brand)
        {
            _context.Brands.Update(brand);
            _context.SaveChanges();
            return Json("Succesfully.");
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.Id == id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
                _context.SaveChanges();
                return Json("Succesfully.");
            }
            return Json("Not Found.");

        }
    }
}

