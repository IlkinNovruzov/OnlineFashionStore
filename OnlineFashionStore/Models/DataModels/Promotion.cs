using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFashionStore.Models.DataModels
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        [Required]
        public decimal DiscountPercentage { get; set; }
        [Required]
        public int UsageLimit { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
