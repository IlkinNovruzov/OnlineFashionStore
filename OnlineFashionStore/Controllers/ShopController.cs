using OnlineFashionStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFashionStore.Models.ViewModels;
using OnlineFashionStore.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using X.PagedList;
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
            var pageSize = 12;
            var model = new ShopViewModel()
            {
                Products = await _context.Products.Include(p => p.Images).Include(x => x.Category).Where(p => p.IsActive).ToPagedListAsync(pageNumber, pageSize),
                Categories = _context.Categories.ToList(),
                Colors = _context.Colors.ToList(),
                Sizes = _context.Sizes.ToList(),
                MaxPrice = await _context.Products.MaxAsync(p => p.Price)
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> GetProducts(int[] ctgIds, int[] colorIds, int[] sizeIds, int min, int max)
        {

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
            var products = await query.ToListAsync();
            return PartialView("_ProductList", products);
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _context.Products.Include(p => p.Images).Include(a => a.Attributes).Include(p => p.ProductColors).ThenInclude(pc => pc.Color).Include(p => p.ProductSizes).ThenInclude(ps => ps.Size).FirstOrDefaultAsync(p => p.Id == id);
            var user = await _userManager.GetUserAsync(User);
            var reviews = await _context.Reviews.Where(r => r.ProductId == id).Include(r => r.User).ToListAsync();
            var reviewCount = reviews != null ? reviews.Count : 0;
            var rating = reviews != null && reviews.Any() ? reviews.Average(r => r.Rating) : 0;
            var model = new ProductDetailsVM()
            {
                Product = product,
                Reviews = reviews,
                Review = (user != null && User.Identity.IsAuthenticated) ? new Review { ProductId = id, UserId = user.Id } : null,
                RelatedProducts = await _context.Products.Include(p => p.Category).Where(p => p.CategoryId == product.Id && p.Id != product.Id).Take(5).ToListAsync(),
                NextProductId = _context.Products.Where(p => p.Id > id && p.IsActive).Min(p => (int?)p.Id),
                PreviousProductId = _context.Products.Where(p => p.Id < id && p.IsActive).Max(p => (int?)p.Id),
                ReviewCount = reviewCount,
                Rating = rating
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
            return Json("Added.");

            //return RedirectToAction("ProductDetails", new { id = id });
        }




    }

}
