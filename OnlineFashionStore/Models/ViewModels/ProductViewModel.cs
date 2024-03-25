using OnlineFashionStore.Models.DataModels;

namespace OnlineFashionStore.Models.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<int> ColorIds { get; set; }
    }
}
