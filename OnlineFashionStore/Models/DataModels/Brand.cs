using System.ComponentModel.DataAnnotations;

namespace OnlineFashionStore.Models.DataModels
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
