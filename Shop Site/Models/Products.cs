namespace Shop_Site.Models
{
    public class Products:BaseEntity
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public virtual Brand brand { get; set; } = null!;
        public virtual Category category { get; set; } = null!;
        public string BrandId { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public int AverageRating { get; set; }
        public virtual List<Review> ?Reviews { get; set; }
        public bool IsFavorite { get; set; }
        private int  _stockQuantity;
        public int StockQuantity { get => _stockQuantity; set { _stockQuantity = value; IsAvailable = value > 0; } }
        public bool IsAvailable { get; private set; }
        public int CountProduct { get; set; }
    }
}
