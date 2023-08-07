namespace Shop_Site.Models
{
    public class PurchasedProduct:BaseEntity
    {
        public string ProductId { get; set; } = null!;
        public virtual AppUser User { get; set; }
    }
}
