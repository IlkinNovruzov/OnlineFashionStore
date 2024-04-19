using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFashionStore.Models;

namespace OnlineFashionStore.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult BlogList()
        {
            return View(_context.Blogs.ToList());
        }
        public IActionResult BlogDetails()
        {
            return View();
        }
    }
}
