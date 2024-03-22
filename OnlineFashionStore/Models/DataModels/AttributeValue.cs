using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineFashionStore.Models.DataModels
{
    public class AttributeValue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Value { get; set; }

        [Required]
        public int AttributeId { get; set; }
        public ProductAttribute Attribute { get; set; }

        public bool IsActive { get; set; }

    }
}
