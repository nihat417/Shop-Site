namespace Shop_Site.Models
{
    public class Review : BaseEntity
    {
        public string AuthorName { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int Rating { get; set; }
        public string PurchasedProductId { get; set; } = null!;
        public virtual PurchasedProduct PurchasedProduct { get; set; } = null!;

    }
}
