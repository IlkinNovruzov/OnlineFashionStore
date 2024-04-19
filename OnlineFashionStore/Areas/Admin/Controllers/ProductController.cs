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
            ViewBag.Sizes = _context.Sizes.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel model)
        {

                model.Product.CreatedAt = DateTime.Now;
                await _context.Products.AddAsync(model.Product);
                await _context.SaveChangesAsync();
                foreach (var colorId in model.ColorIds)
                {
                    var productColor = new ProductColor
                    {
                        ProductId = model.Product.Id,
                        ColorId = colorId
                    };
                    _context.ProductColors.Add(productColor);
                }
                foreach (var sizeId in model.SizeIds)
                {
                    var productSize = new ProductSize
                    {
                        ProductId = model.Product.Id,
                        SizeId = sizeId
                    };
                    _context.ProductSizes.Add(productSize);
                }
                _context.SaveChanges();
                return RedirectToAction("Image", new { id = model.Product.Id });
          
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Colors = _context.Colors.ToList();
            ViewBag.Sizes = _context.Sizes.ToList();

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            var colorIds = _context.ProductColors.Where(pc => pc.ProductId == id).Select(pc => pc.ColorId).ToList();
            var sizeIds = _context.ProductSizes.Where(ps => ps.ProductId == id).Select(ps => ps.SizeId).ToList();
            var model = new ProductViewModel()
            {
                Product = product,
                ColorIds = colorIds,
                SizeIds = sizeIds
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == model.Product.Id);
                product.Name = model.Product.Name;
                product.Description = model.Product.Description;
                product.CategoryId = model.Product.CategoryId;
                product.BrandId = model.Product.BrandId;
                product.StockQuantity = model.Product.StockQuantity;
                product.Price = model.Product.Price;
                product.IsActive = model.Product.IsActive;

                await _context.SaveChangesAsync();

                var oldColors = _context.ProductColors.Where(pc => pc.ProductId == model.Product.Id).ToList();
                var oldSizes = _context.ProductSizes.Where(ps => ps.ProductId == model.Product.Id).ToList();
                _context.ProductColors.RemoveRange(oldColors);
                _context.ProductSizes.RemoveRange(oldSizes);
                foreach (var colorId in model.ColorIds)
                {
                    var productColor = new ProductColor
                    {
                        ProductId = model.Product.Id,
                        ColorId = colorId
                    };
                    _context.ProductColors.Add(productColor);
                }
                foreach (var sizeId in model.SizeIds)
                {
                    var productSize = new ProductSize
                    {
                        ProductId = model.Product.Id,
                        SizeId = sizeId
                    };
                    _context.ProductSizes.Add(productSize);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("GetProduct");
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
        public IActionResult Image(int id)
        {
            var model = new ImageViewModel
            {
                Id = id,
                Images = _context.Images.Where(x => x.ProductId == id).ToList()
            };
            return View(model);
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
        public async Task<IActionResult> DeleteImage(int id)
        {
            var img = await _context.Images.FirstOrDefaultAsync(i => i.Id == id);

            if (img != null)
            {
                _context.Images.Remove(img);
            }
            _context.SaveChanges();
            return RedirectToAction("Image", new { id = img.ProductId });

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
       
    }
}
