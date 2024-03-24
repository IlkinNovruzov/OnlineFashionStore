using Microsoft.AspNetCore.Mvc;
using OnlineFashionStore.Models;
using OnlineFashionStore.Models.DataModels;

namespace OnlineFashionStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController : Controller
    {
        private readonly AppDbContext _context;
        public ColorController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult GetColor()
        {
            return View(_context.Colors.ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Color color)
        {
            _context.Colors.Add(color);
            _context.SaveChanges();
            return RedirectToAction("GetColor");
        }
        public IActionResult Delete(int id)
        {
            var c = _context.Colors.Find(id);
            if (c != null)
            {
                _context.Colors.Remove(c);
            }
            _context.SaveChanges();
            return RedirectToAction("GetColor");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_context.Colors.Find(id));
        }
        [HttpPost]
        public IActionResult Edit(Color color)
        {
            var c = _context.Colors.Find(color.Id);
            if (c != null)
            {
                c.Name = color.Name;
                c.Code = color.Code;
                c.IsActive = color.IsActive;
            }
            _context.SaveChanges();
            return RedirectToAction("GetColor");
        }
        public IActionResult Activation(int id)
        {
            var color = _context.Colors.Find(id);
            if (color != null)
            {
                if (color.IsActive)
                {
                    color.IsActive = false;
                }
                else
                {
                    color.IsActive = true;
                }
                _context.SaveChanges();
            }
            return RedirectToAction("GetColor");
        }
    }
}
