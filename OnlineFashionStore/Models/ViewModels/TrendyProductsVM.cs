using OnlineFashionStore.Models.DataModels;

namespace OnlineFashionStore.Models.ViewModels
{
    public class TrendyProductsVM
    {
        public List<Product> NewArrivals { get; set; }
        public List<Product> BestSellers { get; set; }
        public List<Product> TopRated { get; set; }
    }
}
