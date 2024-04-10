using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFashionStore.Extensions;
using OnlineFashionStore.Models;
using OnlineFashionStore.Models.ViewModels;

namespace OnlineFashionStore.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        public CartController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult ShopCart()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };

            return View(cartVM);
        }
        //public async Task<IActionResult> Add(int id)
        //{
        //    var product = await _context.Products.FindAsync(id);

        //    List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

        //    CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

        //    if (cartItem == null)
        //    {
        //        cart.Add(new CartItem(product));
        //    }
        //    else
        //    {
        //        cartItem.Quantity += 1;
        //    }

        //    HttpContext.Session.SetJson("Cart", cart);

        //    TempData["Success"] = "The product has been added!";
        //    return Json(new { success = true });
        //    //return RedirectToAction("ShopCart");
        //}
        [HttpPost]
        public async Task<IActionResult> Add(CartItem cartItem)
        {
            var product =await _context.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == cartItem.ProductId);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem item = cart.FirstOrDefault(c => c.ProductId == cartItem.ProductId);

            if (item == null)
            {
                var newItem = new CartItem(product);
                newItem.Quantity = cartItem.Quantity;
                newItem.Color = cartItem.Color;
                newItem.Size = cartItem.Size;
                cart.Add(newItem);
            }
            else
            {
                item.Quantity += cartItem.Quantity;
            }

            HttpContext.Session.SetJson("Cart", cart);

            TempData["Success"] = "The product has been added!";
            return Json(new { success = true });
            //return RedirectToAction("ShopCart");
        }
        public async Task<IActionResult> Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The product has been removed!";
            return Json(new { success = true });

            //return RedirectToAction("ShopCart");
        }

        public async Task<IActionResult> Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(p => p.ProductId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The product has been removed!";

            return RedirectToAction("ShopCart");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("ShopCart");
        }
    }
}

