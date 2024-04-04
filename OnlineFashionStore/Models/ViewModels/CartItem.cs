using OnlineFashionStore.Models.DataModels;

namespace OnlineFashionStore.Models.ViewModels
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public decimal Total
        {
            get { return Quantity * Price; }
        }

        public CartItem()
        {
        }

        public CartItem(Product product)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            Price = product.Price;
            Quantity = 1;
        }
    }
}
