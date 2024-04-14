using Microsoft.Identity.Client;
using OnlineFashionStore.Models.DataModels;
using X.PagedList;

namespace OnlineFashionStore.Models.ViewModels
{
    public class ShopViewModel
    {
        public IPagedList<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Color> Colors { get; set; }
        public List<Size> Sizes { get; set; }
        public decimal MaxPrice { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => !Products.IsFirstPage;
        public bool HasNextPage => !Products.IsLastPage;
    }
}
