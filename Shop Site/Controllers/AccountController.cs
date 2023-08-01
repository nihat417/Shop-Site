﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop_Site.Models;
using Shop_Site.Models.ViewModel;

namespace Shop_Site.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;

		public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
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
    }
}