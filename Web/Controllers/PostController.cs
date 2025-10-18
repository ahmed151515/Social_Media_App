using Core.Interfaces.Services;
using Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Extension;

namespace Web.Controllers
{
	[Authorize]
	public class PostController(IPostService postService, IMembershipService membershipService) : Controller
	{
		[AllowAnonymous]
		public async Task<IActionResult> Index(int id)
		{
			var post = await postService.GetPostDetailsAsync(id);

			if (post is null)
			{
				return NotFound();
			}




			return View(post);
		}

		[HttpGet]
		public async Task<IActionResult> Create(int communityId)
		{
			var userId = User.GetUserId();

			if (await membershipService.IsJoinAsync(userId, communityId) == false)
			{
				return Forbid();
			}

			var viewModel = new PostViewModel
			{
				CommunityId = communityId
			};



			return View(viewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(PostViewModel post)
		{
			if (ModelState.IsValid)
			{
				var userId = User.GetUserId();

				if (await membershipService.IsJoinAsync(userId, post.CommunityId) == false)
				{
					return Forbid();
				}


				var id = await postService.CreateAsync(post, userId);

				return RedirectToAction("Index", new { id = id });
			}
			return View(post);
		}


		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var userId = User.GetUserId();
			var post = await postService.GetByIdAndUserIdAsync(id, userId);

			if (post is null)
			{
				return Forbid();
			}

			var viewModel = new PostViewModel
			{
				Id = post.Id,
				Title = post.Title,
				Content = post.Content
			};

			return View(viewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(PostViewModel post)
		{
			if (ModelState.IsValid)
			{
				var userId = User.GetUserId();

				if (await postService.IsOwnerAsync(post.Id, userId) == false)
				{
					return Forbid();

				}




				var id = await postService.UpdateAsync(post);



				return RedirectToAction("Index", new { id = id });

			}
			return View(post);
		}
		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var userId = User.GetUserId();
			var post = await postService.GetByIdAndUserIdAsync(id, userId);

			if (post is null)
			{
				return Forbid();
			}

			var viewModel = new PostViewModel
			{
				Id = post.Id,
				Title = post.Title,
				Content = post.Content
			};

			return View(viewModel);
		}
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{


			var userId = User.GetUserId();

			if (await postService.IsOwnerAsync(id, userId) == false)
			{
				return Forbid();

			}




			await postService.DeleteAsync(id);



			return RedirectToAction("Profile", "Account");




		}
	}
}
