namespace Shop_Site.Models
{
    public class CartItem
    {
        public Products Product { get; set; } = null!;
        public int Quantity { get; set; }

        public static decimal CalculateTotalPrice(List<CartItem> cart)
        {
            decimal totalPrice = 0;
            foreach (var item in cart)
            {
                totalPrice += item.Product.Price * item.Quantity;
            }
            return totalPrice;
        }
    }

}
