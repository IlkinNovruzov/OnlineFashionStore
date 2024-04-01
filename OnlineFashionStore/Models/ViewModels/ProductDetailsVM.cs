using OnlineFashionStore.Models.DataModels;

namespace OnlineFashionStore.Models.ViewModels
{
    public class ProductDetailsVM
    {
        public Product Product { get; set; }
        public List<Review> Reviews { get; set; }
        public Review? Review { get; set; }
    }
}
