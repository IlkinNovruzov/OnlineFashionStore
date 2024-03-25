using OnlineFashionStore.Models.DataModels;
namespace OnlineFashionStore.Models.ViewModels
{
    public class ImageViewModel
    {
        public int Id { get; set; }
        public List<Image> Images { get; set; }
        public IFormFile ImgFile { get; set; }
    }
}
