using System.ComponentModel.DataAnnotations;

namespace Shop_Site.Models.ViewModel
{
    public class AddProductViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
    }
}
