using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.ViewComponents
{
	public class UserCommunitiesViewComponent(IUserService userService) : ViewComponent
	{

		public async Task<IViewComponentResult> InvokeAsync()
		{
			if (User.Identity.IsAuthenticated != true)
			{
				return View(new List<Community>());
			}
			var userId = UserClaimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;


			var Communities = await userService.GetCommunitiesOfUserByIDAsync(userId);

			return View(Communities);
		}
	}
}
