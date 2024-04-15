using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFashionStore.Models;
using OnlineFashionStore.Models.ViewModels;

namespace OnlineFashionStore.ViewCompanents
{
    public class TrendyProductsVC : ViewComponent
    {
        private readonly AppDbContext _context;
        public TrendyProductsVC(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var trendyProducts = new TrendyProductsVM();

            var bestSellingProductIds = await _context.OrderItems.GroupBy(oi => oi.ProductId).OrderByDescending(group => group.Sum(oi => oi.Quantity)).Take(10).Select(group => group.Key).ToListAsync();

            trendyProducts.BestSellers = await _context.Products.Include(p=>p.Images).Include(p => p.Category).Where(p => bestSellingProductIds.Contains(p.Id)).ToListAsync();

            trendyProducts.NewArrivals =await _context.Products.Include(p => p.Images).Include(p => p.Category).OrderByDescending(p => p.CreatedAt) .Take(10).ToListAsync();

            trendyProducts.TopRated = _context.Products.Include(p => p.Images).Include(p => p.Category).Where(p => p.Reviews.Any()) .OrderByDescending(p => p.Reviews.Average(r => r.Rating)).Take(10).ToList();

            return View(trendyProducts);
        }
    }
}
