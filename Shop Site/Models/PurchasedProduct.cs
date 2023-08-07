namespace Shop_Site.Models
{
    public class PurchasedProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual AppUser User { get; set; }
    }
}
