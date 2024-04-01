using OnlineFashionStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFashionStore.Models.ViewModels;
using OnlineFashionStore.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using OnlineFashionStore.Extensions;
namespace OnlineFashionStore.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private static SignInManager<AppUser> SignInManager;
        public ShopController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> GetProducts()
        {
            var model = new ShopViewModel()
            {
                Products = await _context.Products.Include(p => p.Images).Include(x => x.Category).Where(x => x.IsActive == true).ToListAsync(),
                Categories = _context.Categories.ToList(),
                Brands = _context.Brands.ToList(),
                Colors = _context.Colors.ToList(),
                BrandProductCounts = _context.Products.GroupBy(p => p.Brand.Name).Select(g => new BrandProductCount { BrandName = g.Key, ProductCount = g.Count() }).ToList()
            };
            return View(model);
        }
        public async Task<IActionResult> ProductDetails(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new ProductDetailsVM()
            {
                Product = await _context.Products.Include(p => p.Images).Include(a => a.Attributes).Include(p => p.ProductColors).ThenInclude(pc => pc.Color).Include(p => p.ProductSizes).ThenInclude(ps => ps.Size).FirstOrDefaultAsync(p => p.Id == id),
                Reviews = _context.Reviews.Where(r => r.ProductId == id).Include(r => r.User).ToList(),
                Review = (user != null && SignInManager.IsSignedIn(User)) ? new Review { ProductId = id, UserId = user.Id } : null
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
            return RedirectToAction("ProductDetails",new { id=id});
        }
       
        
        
        public IActionResult AddBrand(string Bname)
        {
            try
            {
                _context.Brands.Add(new Brand { Name=Bname});
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }

}
