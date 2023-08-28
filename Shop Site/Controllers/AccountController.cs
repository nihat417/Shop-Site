using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop_Site.Models;
using Shop_Site.Models.ViewModel;
using Shop_Site.Repository.Interfaces;

namespace Shop_Site.Controllers
{
    public class AccountController : Controller
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;
		private readonly IEmailService _emailService;

		public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,IEmailService _emailService)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this._emailService = _emailService;
		}


		public IActionResult Register() =>View();
  
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
					var token=await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",new { token, email = user.Email },Request.Scheme);

                    var message = new Message(new string[] { user.Email }, "Confirmation Email Link", confirmationLink!);
                    _emailService.SendEmail(message);
					var nwvm = new RegisterViewModel { UserName=user.UserName };
					return View("RegisterFinish",nwvm);
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
            }
			return View(vm);
		}

		public IActionResult Login(string ReturnUrl) 
		{
			ViewBag.ReturnUrl = ReturnUrl;
			return View();
		}
		
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel vm,string? ReturnUrl)
		{
			if(ModelState.IsValid)
			{
				var user =await userManager.FindByEmailAsync(vm.Email);
				if (user is not null)
				{
					if(await userManager.IsEmailConfirmedAsync(user))
					{
                        if (user.LockoutEnabled == true) 
                        {
                            var result = await signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);

                            if (result.Succeeded)
                            {
							    if(await userManager.IsInRoleAsync(user,"Admin"))
							    {
								    return Redirect("/foradmin");
							    }

                                if (!string.IsNullOrWhiteSpace(ReturnUrl))
                                    return Redirect(ReturnUrl);
                                return Redirect("/");
                            }
                            else if (result.IsLockedOut)
                                ModelState.AddModelError("All", "Lockout");
                        }
                        else { return View("BlockPage"); }
                    }
                    else
                        ModelState.AddModelError("All", "Mail has not confired yet!!");
                }
                else
                    ModelState.AddModelError("login", "Incorrect username or password");
            }

			return View(vm);
		}

		public async Task<ActionResult> Logout()
		{
			await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account", new { area = "" });

        }

		[HttpGet]
        public async Task<IActionResult>ConfirmEmail(string token,string email)
		{
			var user=await userManager.FindByEmailAsync(email);
			if(user != null)
			{
				var result = await userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    var vm= new RegisterViewModel { UserName = user.UserName};
                    return View("SuccessPage",vm);
                }
            }
            return View();
        }

		public IActionResult ForgotPassword() => View();

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> ForgotPassword(ForgotViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(vm.Email);
                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var newlink = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);
                    var message = new Message(new string[] { user.Email }, "Forgot password link", newlink!);
                     _emailService.SendEmail(message); 
                    return View("ForgotPasswordConfirmation");
                }
                ModelState.AddModelError(string.Empty, "User not found");
            }
            return View();
        }

        public IActionResult ResetPassword(string token ,string email)
        {
            var viewModel = new ResetPasswordViewModel
            {
                Token = token,
                Email = email
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(vm.Email);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "User not found");
                    return View(vm);
                }

                var result = await userManager.ResetPasswordAsync(user, vm.Token, vm.Password);

                if (result.Succeeded)
                {
                    return View("ResetPasswordConfirmation");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(vm);
        }

    }
}
