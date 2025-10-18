using Core.Eunm;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Extension;

namespace Web.Controllers
{
	[Authorize]
	public class CommunityController(
		ICommunityService communityService,
		UserManager<ApplicationUser> userManager,
		IMembershipService membershipService
		) : Controller
	{
		[AllowAnonymous]
		public async Task<IActionResult> Index(int page = 1)
		{


			var viewModel = await communityService.PagingAsync(page);
			return View(viewModel);
		}
		[AllowAnonymous]
		public async Task<IActionResult> Details(int id, int page)
		{


			var viewModel = await communityService.GetCommunityDetailsAsync(id, page);
			if (User.Identity.IsAuthenticated)
			{
				var userId = User.GetUserId();
				viewModel.IsJoin = await membershipService.IsJoinAsync(userId, id);

			}

			return View(viewModel);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Community community)
		{
			if (ModelState.IsValid)
			{
				var userId = User.GetUserId();
				await communityService.CreateAsync(community, userId);

				return RedirectToAction("Details", new { id = community.Id });

			}

			return View(community);
		}

		public async Task<IActionResult> Join(int id)
		{
			var userId = User.Claims.Single(e => e.Type == ClaimTypes.NameIdentifier).Value;

			var membership = new Membership
			{
				CommunityId = id,
				UserId = userId,
				CommunityRole = CommunityRole.Member
			};

			await membershipService.JoinAsync(membership);

			return RedirectToAction("Details", new
			{
				id
			});
		}
		public async Task<IActionResult> Leave(int id)
		{
			var userId = User.Claims.Single(e => e.Type == ClaimTypes.NameIdentifier).Value;

			await membershipService.LeaveAsync(userId, id);

			return RedirectToAction("Details", new
			{
				id
			});
		}
	}
}
