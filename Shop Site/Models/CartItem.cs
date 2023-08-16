namespace Shop_Site.Models
{
    public class CartItem
    {
        public Products Product { get; set; } = null!;
        public int Quantity { get; set; }
    }

}
