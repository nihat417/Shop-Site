using System.ComponentModel.DataAnnotations;

namespace Shop_Site.Models.ViewModel
{
    public class AddProductViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public string CategoryId { get; set; } = null!;
        public string BrandId { get; set; } = null!;
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage ="Stock Quantity Is required")]
        [Range(0,int.MaxValue,ErrorMessage ="Quantity must be positive")]
        public int StockQuantity { get; set; }

    }
}
