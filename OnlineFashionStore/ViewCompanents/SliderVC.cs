using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFashionStore.Models;

namespace OnlineFashionStore.ViewCompanents
{
    public class SliderVC:ViewComponent
    {
        private readonly AppDbContext _context;
        public SliderVC(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = await _context.Sliders.ToListAsync();
            return View(sliders);
        }
    }
}
