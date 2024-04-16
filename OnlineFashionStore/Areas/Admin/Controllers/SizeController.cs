using Microsoft.AspNetCore.Mvc;
using OnlineFashionStore.Models.DataModels;
using OnlineFashionStore.Models;

namespace OnlineFashionStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizeController : Controller
    {
        private readonly AppDbContext _context;
        public SizeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult SizeList()
        {
            return Json(_context.Sizes.ToList());
        }
        [HttpPost]
        public JsonResult Add(Size size)
        {
            _context.Sizes.Add(size);
            _context.SaveChanges();
            return Json("Added.");
        }
        [HttpGet]
        public JsonResult Edit(int id)
        {
            var size = _context.Sizes.FirstOrDefault(s => s.Id == id);
            return Json(size);
        }
        [HttpPost]
        public JsonResult Update(Size size)
        {
            _context.Sizes.Update(size);
            _context.SaveChanges();
            return Json("Succesfully.");
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var size = _context.Sizes.FirstOrDefault(s => s.Id == id);
            if (size != null)
            {
                _context.Sizes.Remove(size);
                _context.SaveChanges();
                return Json("Succesfully.");
            }
            return Json("Not Found.");

        }
    }
}
