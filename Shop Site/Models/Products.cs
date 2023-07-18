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
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
    }
}
