namespace Shop_Site.Models
{
    public class PurchasedProduct : BaseEntity
    {
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public int ProductCount { get; set; }
        public virtual AppUser? User { get; set; }

    }
}
