using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineFashionStore.Models.DataModels
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Context { get; set; }
        [NotMapped]
        public IFormFile ImgFile { get; set; }
    }
}
