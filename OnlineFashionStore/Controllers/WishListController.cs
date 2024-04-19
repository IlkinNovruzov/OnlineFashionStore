using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineFashionStore.Extensions;
using OnlineFashionStore.Models;
using OnlineFashionStore.Models.DataModels;
using OnlineFashionStore.Models.ViewModels;

namespace OnlineFashionStore.Controllers
{
    public class WishListController : Controller
    {
        private readonly AppDbContext _context;
        public WishListController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult WishProducts()
        {
            List<Product> wishList = HttpContext.Session.GetJson<List<Product>>("Wish") ?? new List<Product>();
            return View(wishList);
        }
        [HttpPost]
        public async Task<IActionResult> Add(int id)
        {
            var product = await _context.Products.Include(p=>p.Images).FirstOrDefaultAsync(p=>p.Id==id);
            List<Product> wishList = HttpContext.Session.GetJson<List<Product>>("Wish") ?? new List<Product>();
            if (!wishList.Contains(product))
            {
                wishList.Add(product);
            }
            HttpContext.Session.SetString("Wijkjsh", JsonConvert.SerializeObject(wishList));

            TempData["Success"] = "The product has been added!";
            return Json(new { success = true });
        }
        public async Task<IActionResult> Remove(int id)
        {
            List<Product> wishList = HttpContext.Session.GetJson<List<Product>>("Wish");
            var product = await _context.Products.FindAsync(id);

            wishList.Remove(product);

            if (wishList.Count == 0)
            {
                HttpContext.Session.Remove("Wish");
            }
            else
            {
                HttpContext.Session.SetJson("Wish", wishList);
            }

            TempData["Success"] = "The product has been removed!";
            return Json(new { success = true });
        }
    }
}
