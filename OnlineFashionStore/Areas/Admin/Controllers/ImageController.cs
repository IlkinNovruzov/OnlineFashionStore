using OnlineFashionStore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFashionStore.Models;
using OnlineFashionStore.Models.DataModels;

namespace OnlineFashionStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ImageController : Controller
    {
        private readonly AppDbContext _context;
        public ImageController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Images.Include(x => x.Product).ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Products = _context.Products.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Image image)
        {
            if (FileExtensions.IsImage(image.ImageFile))
            {
                string nameImg = await FileExtensions.SaveAsync(image.ImageFile, "products");
                image.ImageUrl = nameImg;
                _context.Images.Add(image);
                _context.SaveChanges();
            }
            else
            {
                return RedirectToAction("Add");
            }
            return RedirectToAction("Index");
        }
    }
}
