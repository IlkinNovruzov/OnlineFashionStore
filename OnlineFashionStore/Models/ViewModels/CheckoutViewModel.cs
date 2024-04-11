using OnlineFashionStore.Models.DataModels;

namespace OnlineFashionStore.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public Order Order { get; set; }
        public List<OrderItem> OrderItems { get; set; }
       
    }
}
