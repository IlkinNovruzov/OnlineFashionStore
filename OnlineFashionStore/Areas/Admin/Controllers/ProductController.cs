using OnlineFashionStore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFashionStore.Models;
using OnlineFashionStore.Models.DataModels;
using OnlineFashionStore.Models.ViewModels;

namespace OnlineFashionStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult GetProduct()
        {
            return View(_context.Products.Include(x => x.Brand).Include(x => x.Category).ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Colors = _context.Colors.ToList();
            return View();
        }
        [HttpGet]
        public IActionResult Image(int id)
        {
            var model = new ImageViewModel
            {
                Id = id,
                Images = _context.Images.Where(x => x.ProductId == id).ToList()
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Attributes(int id)
        {
            var model = new AttributeViewModel
            {
                ProductId = id,
                ProductAttributes = _context.ProductAttributes.Where(x => x.ProductId == id).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddAttribute(AttributeViewModel model)
        {
          
                var attribute = new ProductAttribute
                {
                    Name = model.productAttribute.Name,
                    Value = model.productAttribute.Value,
                    ProductId = model.ProductId,
                    IsActive = model.productAttribute.IsActive
                };
                _context.ProductAttributes.Add(attribute);
                _context.SaveChanges();
            
            return RedirectToAction("Attributes", new { id = model.ProductId });

        }
        [HttpPost]
        public async Task<IActionResult> AddImage(ImageViewModel model)
        {
            if (FileExtensions.IsImage(model.ImgFile))
            {
                string nameImg = await FileExtensions.SaveAsync(model.ImgFile, "products");
                var productImage = new Image
                {
                    ImageUrl = nameImg,
                    ProductId = model.Id,
                    IsActive = true
                };
                _context.Images.Add(productImage);
                _context.SaveChanges();
            }
            return RedirectToAction("Image", new { id = model.Id });

        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel model)
        {
            _context.Products.Add(model.Product);
            _context.SaveChanges();
            foreach (var colorId in model.ColorIds)
            {
                var productColor = new ProductColor
                {
                    ProductId = model.Product.Id,
                    ColorId = colorId
                };
                _context.ProductColors.Add(productColor);
            }
            _context.SaveChanges();
            return RedirectToAction("GetProduct");
        }
        public async Task<IActionResult> DeleteAttribute(int id)
        {
            var a = await _context.ProductAttributes.FirstOrDefaultAsync(p => p.Id == id);

            if (a != null)
            {
                _context.ProductAttributes.Remove(a);
            }
            _context.SaveChanges();
            return RedirectToAction("Attributes", new { id = id });

        }
        public async Task<IActionResult> DeleteImage(int id)
        {
            var p = await _context.Images.FirstOrDefaultAsync(p => p.Id == id);

            if (p != null)
            {
                _context.Images.Remove(p);
            }
            _context.SaveChanges();
            return RedirectToAction("Image", new { id = id });

        }
        public IActionResult Delete(int id)
        {
            var p = _context.Products.Find(id);
            if (p != null)
            {
                _context.Products.Remove(p);
            }
            _context.SaveChanges();
            return RedirectToAction("GetProduct");
        }
        [HttpGet]
        public async Task<IActionResult> EditAsync(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Colors = _context.Colors.ToList();

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            var colorIds = _context.ProductColors.Where(pc => pc.ProductId == id).Select(pc => pc.ColorId).ToList();
            var model = new ProductViewModel()
            {
                Product = product,
                ColorIds = colorIds
            };
           
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            _context.Products.Update(model.Product);
          //  var colorIds = _context.ProductColors.Where(pc => pc.ProductId == id).Select(pc => pc.ColorId).ToList();
         
            //_context.ProductColors.Update(model.ColorIds);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetProduct");
        }
        [HttpPost]
        public IActionResult Activation(int productId, bool isActive)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                product.IsActive = isActive;
                _context.SaveChanges();
            }
            return RedirectToAction("GetProduct");
        }
    }
}
