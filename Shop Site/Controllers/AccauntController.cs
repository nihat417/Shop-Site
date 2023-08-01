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

		public async Task<IActionResult> Register(AddProductViewModel viewModel)
		{
			return View();
		}

		[Route("Accaunt/Login")]
		public IActionResult Login() 
		{
			return View();
		}

		public async Task<IActionResult> Login(AddProductViewModel viewModel)
		{
			return View();
		}

		public ActionResult Logout()
		{
			return View();
		}
	}
}
