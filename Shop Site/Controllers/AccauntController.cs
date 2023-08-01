using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop_Site.Models;
using Shop_Site.Models.ViewModel;

namespace Shop_Site.Controllers
{
	public class AccauntController : Controller
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;

		public AccauntController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		[Route("Accaunt/Register")]
		public IActionResult Register() =>View();

        [Route("Accaunt/Register")]
        [HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel vm)
		{
			if(ModelState.IsValid)
			{
				AppUser user = new()
				{
					FullName = vm.UserName,
					UserName = vm.UserName,
					Email = vm.Email,
					CreatedDate=DateTime.Now
				};
				var result=await userManager.CreateAsync(user,vm.Password);
				if(result.Succeeded)
				{
                    await signInManager.SignInAsync(user, false);
					return RedirectToAction("Index", "Shop");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
            }
			return View(vm);
		}

		[Route("Accaunt/Login")]
		public IActionResult Login(string ReturnUrl) 
		{
			ViewBag.ReturnUrl = ReturnUrl;
			return View();
		}

		public async Task<IActionResult> Login(LoginViewModel vm)
		{
			return View();
		}

		public ActionResult Logout()
		{
			return View();
		}
	}
}
