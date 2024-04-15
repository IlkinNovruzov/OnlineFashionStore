using OnlineFashionStore.Models.DataModels;

namespace OnlineFashionStore.Models.ViewModels
{
    public class ProductDetailsVM
    {
        public Product Product { get; set; }
        public List<Review> Reviews { get; set; }
        public Review? Review { get; set; }
        public CartItem CartItem { get; set; }
        public int? NextProductId { get; set; }
        public int? PreviousProductId { get; set; }
        public int ReviewCount { get; set; }
        public double Rating { get; set; }
    }
}
