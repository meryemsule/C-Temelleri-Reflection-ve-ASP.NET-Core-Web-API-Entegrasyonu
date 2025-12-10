using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, 10000.0)]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}