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
           // ViewBag.Attributes = _context.ProductAttributes.Include(x=>x.Values).ToList();
            return View();
        }

        //[HttpPost]
        //public IActionResult AttributeAdd(List<Attribute> attributes)
        //{

        //    return RedirectToAction("Add");
        //}
        //[HttpPost]
        //public async Task<IActionResult> Add(ProductViewModel model)
        //{
        //    var product= new Product
        //    {
        //        Name = model.Name,
        //        Description = model.Description,
        //        Price = model.Price,
        //        StockQuantity= model.StockQuantity,
        //        CategoryId= model.CategoryId,
        //        BrandId= model.BrandId,
        //        IsActive= model.IsActive,
        //    };
        //    _context.Products.Add(product);
        //    _context.SaveChanges();
        //    foreach (var image in model.ProductImages)
        //    {
        //        if (FileExtensions.IsImage(image))
        //        {
        //            string nameImg = await FileExtensions.SaveAsync(image, "products");
        //            var productImage = new Image
        //            {
        //                ImageUrl = nameImg,
        //                ProductId = product.Id,
        //                IsActive=true
        //            };

        //            _context.Images.Add(productImage);
        //        }
        //    }
        //    _context.SaveChanges();
        //    return RedirectToAction("GetProduct");
        //}
        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel model)
        {
            _context.Products.Add(model.Product);
            _context.SaveChanges();
            foreach (var image in model.ProductImages)
            {
                if (FileExtensions.IsImage(image))
                {
                    string nameImg = await FileExtensions.SaveAsync(image, "products");
                    var productImage = new Image
                    {
                        ImageUrl = nameImg,
                        ProductId = model.Product.Id,
                        IsActive = true
                    };
                    _context.Images.Add(productImage);
                }
            }
            //var attributes = _context.ProductAttributes.Include(a => a.Values).ToList();
            //foreach (var attribute in attributes)
            //{
            //        var selectedValues = attribute.Values.Where(v => model.AttributeValueIds.Contains(v.Id)).ToList();
            //            var att = new ProductAttribute
            //            {
            //                Name = attribute.Name,
            //                ProductId = attribute.Id,
            //                IsActive = true,
            //                Values=selectedValues
            //            };
                  
            //}
            _context.SaveChanges();
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
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();

            var product = _context.Products.Find(id);
            var images = _context.Images .Where(x => x.ProductId == id).Select(x=>x.ImageFile).ToList();
            ProductViewModel model = new ProductViewModel();
            model.Product = product;
            model.ProductImages = images;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            _context.Products.Add(model.Product);
            _context.SaveChanges();
            foreach (var image in model.ProductImages)
            {
                if (FileExtensions.IsImage(image))
                {
                    string nameImg = await FileExtensions.SaveAsync(image, "products");
                    var productImage = new Image
                    {
                        ImageUrl = nameImg,
                        ProductId = model.Product.Id,
                        IsActive = true
                    };
                    _context.Images.Add(productImage);
                }
            }
            _context.SaveChanges();
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
