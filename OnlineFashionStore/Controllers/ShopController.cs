using OnlineFashionStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFashionStore.Models.ViewModels;
using OnlineFashionStore.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using OnlineFashionStore.Extensions;
using X.PagedList;
using System.Drawing.Printing;
namespace OnlineFashionStore.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ShopController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 1;
            var model = new ShopViewModel()
            {
                Products = await _context.Products.Include(p => p.Images).Include(x => x.Category).Where(x => x.IsActive == true).ToPagedListAsync(pageNumber, pageSize),
                Categories = _context.Categories.ToList(),
                Colors = _context.Colors.ToList(),
                Sizes = _context.Sizes.ToList(),
                MaxPrice = await _context.Products.MaxAsync(p => p.Price)
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> GetProducts(int? page,int[] ctgIds, int[] colorIds, int[] sizeIds, int min, int max)
        {
            var pageNumber = page ?? 1;
            var pageSize = 1;
            var query = _context.Products.Include(p => p.Category).Include(p => p.Images)
                .Include(p => p.ProductColors).ThenInclude(pc => pc.Color)
                .Include(p => p.ProductSizes).ThenInclude(ps => ps.Size)
                .Where(p => p.IsActive == true);

            if (ctgIds.Length > 0)
            {
                query = query.Where(p => ctgIds.Contains(p.CategoryId));
            }
            if (colorIds.Length > 0)
            {
                query = query.Where(p => p.ProductColors.Any(pc => colorIds.Contains(pc.ColorId)));
            }
            if (sizeIds.Length > 0)
            {
                query = query.Where(p => p.ProductSizes.Any(ps => sizeIds.Contains(ps.SizeId)));
            }
            if (max != 0)
            {
                query = query.Where(p => (p.Price <= max && p.Price >= min));
            }
            var products = await query.ToPagedListAsync(pageNumber, pageSize);
            return PartialView("_ProductList", products);
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new ProductDetailsVM()
            {
                Product = await _context.Products.Include(p => p.Images).Include(a => a.Attributes).Include(p => p.ProductColors).ThenInclude(pc => pc.Color).Include(p => p.ProductSizes).ThenInclude(ps => ps.Size).FirstOrDefaultAsync(p => p.Id == id),
                Reviews = _context.Reviews.Where(r => r.ProductId == id).Include(r => r.User).ToList(),
                Review = (user != null && User.Identity.IsAuthenticated) ? new Review { ProductId = id, UserId = user.Id } : null
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult AddReview(Review review)
        {
            int id = review.ProductId;
            review.Date = DateTime.Now;
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return RedirectToAction("ProductDetails", new { id = id });
        }




    }

}
