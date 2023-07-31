using Microsoft.AspNetCore.Identity;

namespace Shop_Site.Models
{
	public class AppUser:IdentityUser
	{
		public string FullName { get;set; }
		public DateTimeKind CreatedDate {  get; set; }
	}
}
