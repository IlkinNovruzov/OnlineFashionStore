using Microsoft.Identity.Client;
using OnlineFashionStore.Models.DataModels;

namespace OnlineFashionStore.Models.ViewModels
{
    public class ShopViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Color> Colors { get; set; }
        public List<BrandProductCount> BrandProductCounts { get; set; }
    }
}
