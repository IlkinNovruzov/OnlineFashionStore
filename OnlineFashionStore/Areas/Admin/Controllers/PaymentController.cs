using Microsoft.AspNetCore.Mvc;
using OnlineFashionStore.Models;
using OnlineFashionStore.Models.DataModels;

namespace OnlineFashionStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PaymentController : Controller
    {
        private readonly AppDbContext _context;
        public PaymentController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Payments.ToList());
        }

        // Ürün detaylarını getirme
        [HttpGet]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Payments.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                return Json(new { name = product.Name});
            }
            return NotFound();
        }

        // Ürün ekleme
        [HttpPost]
        public IActionResult AddProduct(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var newPayment = new Payment {  Name = name};
                _context.Payments.Add(newPayment);
                return Ok();
            }
            return BadRequest();
        }

        // Ürün güncelleme
        [HttpPost]
        public IActionResult UpdateProduct(int id, string name)
        {
            var product = _context.Payments.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.Name = name;
                return Ok();
            }
            return NotFound();
        }

        // Ürün silme
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Payments.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _context.Payments.Remove(product);
                return Ok();
            }
            return NotFound();
        }
    }
}
