using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineFashionStore.Models.DataModels
{
    public class Size
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
