using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFashionStore.Models.DataModels
{
    public class ProductAttribute
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public List<AttributeValue> Values { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsActive { get; set; }

    }
}
