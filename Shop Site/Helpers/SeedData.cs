using Microsoft.AspNetCore.Identity;
using Shop_Site.Models;

namespace Shop_Site.Helpers
{
	public static class SeedData
	{
		public static async Task InitializeAsync(IServiceProvider services)
		{
			using (var scope = services.CreateScope())
			{
				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				if (!await roleManager.RoleExistsAsync("Admin"))
				{
					var result = await roleManager.CreateAsync(new IdentityRole("Admin"));
					if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
				}

				var user = await userManager.FindByEmailAsync("admin@admin.com");
				if (user is null)
				{
					user = new AppUser
					{
						UserName = "admin@admin.com",
						Email = "admin@admin.com",
						FullName = "Admin",
						CreatedDate = DateTime.Now,
						EmailConfirmed = true
					};
					var result = await userManager.CreateAsync(user, "Admin12!");
					if (!result.Succeeded) throw new Exception(result.Errors.First().Description);

					result = await userManager.AddToRoleAsync(user, "Admin");
				}
			}
		}
	}

}
