using Microsoft.AspNetCore.Identity;

namespace Shop_Site.Models
{
	public class AppUser:IdentityUser
	{
		public string FullName { get; set; } = null!;
		public DateTime CreatedDate {  get; set; }
        public virtual List<PurchasedProduct> ?PurchasedProducts { get; set; }
        public virtual List<FavoriteProduct> ?FavoriteProducts { get; set; }
    }
}
