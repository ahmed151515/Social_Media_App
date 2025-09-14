using Core.Interfaces.Services;
using Core.Models;
using Core.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
	public class AccountController
		(
			SignInManager<ApplicationUser> signInManager,
			UserManager<ApplicationUser> userManager,
			IUserService userService
		) : Controller
	{
		public async Task<IActionResult> Profile(string? userName, int page = 1)
		{
			int size = 20;

			if (page < 1) page = 1;



			var viewModel = new ProfileViewModel();


			if (await userService.IsExistByNameAsync(userName))
			{
				viewModel.Name = userName;
				viewModel.posts = await userService.GetPostsOfUserByUserNameAsync(userName, page, size);
				viewModel.IsOwner = false;

			}
			else if (User.Identity.IsAuthenticated)
			{
				viewModel.Name = User.Identity.Name;
				var userId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
				viewModel.posts = await userService.GetPostsOfUserByIdAsync(userId, page, size);

				viewModel.IsOwner = true;
			}
			else
			{
				return NotFound();
			}
			return View(viewModel);
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel registerModel)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					UserName = registerModel.UserName,
					Email = registerModel.Email,
				};
				var res = await userManager.CreateAsync(user, registerModel.Password);

				if (res.Succeeded)
				{
					return RedirectToAction("Login");
				}
				else
				{
					foreach (var error in res.Errors)
					{

						if (error.Code.Contains("UserName"))
						{
							ModelState.AddModelError("UserName", error.Description);
						}
						if (error.Code.Contains("Email"))
						{
							ModelState.AddModelError("Email", error.Description);
						}
						if (error.Code.Contains("Password"))
						{
							ModelState.AddModelError("Password", error.Description);
						}
						else
						{
							ModelState.AddModelError("", error.Description);
						}
					}
				}
			}

			return View(registerModel);
		}
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel loginModel)
		{
			if (ModelState.IsValid)
			{

				var user = await userManager.FindByEmailAsync(loginModel.Email);

				if (user is not null)
				{

					var res = await userManager.CheckPasswordAsync(user, loginModel.Password);
					if (res)
					{
						await signInManager.SignInAsync(user, loginModel.RemembeMe);
						return RedirectToAction("Index", "Home");
					}

				}

				ModelState.AddModelError("", "Email Or Password is wrong");

			}

			return View(loginModel);
		}

		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

	}
}
