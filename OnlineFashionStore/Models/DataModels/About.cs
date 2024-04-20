using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFashionStore.Models.DataModels
{
    public class About
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Story { get; set; }
        [Required]
        public string Mission { get; set; }
        [Required]
        public string Vision { get; set; }
        [NotMapped]
        public IFormFile ImgFile { get; set; }

    }
}
