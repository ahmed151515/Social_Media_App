using Core.Interfaces.Services;
using Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Extension;

namespace Web.Controllers
{
	[Authorize]
	public class CommentController(ICommentService commentService) : Controller
	{
		[AllowAnonymous]
		public async Task<IActionResult> GetCommentReplies(int id)
		{
			var viewModel = await commentService.GetCommentRepliesAsync(id);

			return PartialView("_GetCommnetRepliesPartial", viewModel);
		}

		[HttpGet]
		public IActionResult Create(int? parentarId, int postid)
		{
			var viewModel = new CommentCreateEditDeleteViewModel { PostId = postid, ParentCommentId = parentarId };

			return View(viewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CommentCreateEditDeleteViewModel comment)
		{
			if (ModelState.IsValid)
			{
				var userId = User.GetUserId();



				await commentService.CreateAsync(comment, userId);

				return RedirectToAction("Index", "Post", new { id = comment.PostId });
			}



			return View(comment);
		}
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var userId = User.GetUserId();
			var comment = await commentService.GetByIdAndUserIdAsync(id, userId);

			if (comment is null)
			{
				return Forbid();
			}

			var viewModel = new CommentCreateEditDeleteViewModel
			{
				Id = comment.Id,
				ParentCommentId = comment.ParentCommentId,
				PostId = comment.PostId,
				Content = comment.Content,
			};

			return View(viewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(CommentCreateEditDeleteViewModel comment)
		{
			if (ModelState.IsValid)
			{
				var userId = User.GetUserId();

				if (await commentService.IsOwnerAsync(comment.Id, userId) == false)
				{
					return Forbid();
				}

				await commentService.Update(comment);

				return RedirectToAction("Inedx", "Post", new { id = comment.PostId });
			}



			return View(comment);
		}
		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var userId = User.GetUserId();
			var comment = await commentService.GetByIdAndUserIdAsync(id, userId);

			if (comment is null)
			{
				return Forbid();
			}

			var viewModel = new CommentCreateEditDeleteViewModel
			{
				Id = comment.Id,
				ParentCommentId = comment.ParentCommentId,
				PostId = comment.PostId,
				Content = comment.Content,
			};

			return View(viewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(CommentCreateEditDeleteViewModel comment)
		{

			var userId = User.GetUserId();

			if (await commentService.IsOwnerAsync(comment.Id, userId) == false)
			{
				return Forbid();
			}

			await commentService.Delete(comment);

			return RedirectToAction("Inedx", "Post", new { id = comment.PostId });





		}

		//public async Task<IActionResult> Test()
		//{
		//	var x = await commentRepository.GetAll()
		//		.Include(e => e.Replies)
		//		.Include(e => e.User)
		//		.Include(e => e.ParentComment)
		//		.ThenInclude(e => e.User)
		//		.ToListAsync();

		//	var s = "";

		//	foreach (var item in x)
		//	{
		//		s += $"Id: {item.Id}\n";

		//		s += $"UserName: {item.User.UserName}\n";
		//		s += $"ParentCommentId: {item.ParentCommentId}\n";
		//		s += $"ParentComment Content: {item.ParentComment?.Content}\n";
		//		s += $"current Content: {item.Content}\n";
		//		s += $"Replies: {item.Replies.Count}\n";
		//		s += $"ParentComment UserName: {item.ParentComment?.User.UserName ?? null}\n";

		//		s += "--------\n";
		//	}




		//	return Content(s);
		//}
	}
}
