namespace Shop_Site.Models
{
    public class FavoriteProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual AppUser User { get; set; }
    }
}
