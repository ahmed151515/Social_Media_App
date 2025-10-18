using Core.Constants;
using Core.Extension;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Core.Models;
using Core.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using X.PagedList.EF;

namespace Services
{
	public class UserService(UserManager<ApplicationUser> userManager, IPostRepository postRepository) : IUserService
	{
		public async Task<List<Community>> GetCommunitiesOfUserByIDAsync(string userId)
		{

			var Communities = await userManager.Users.Where(u => u.Id == userId).SelectMany(u => u.Communities).ToListAsync();

			return Communities;
		}

		public async Task<ProfileViewModel?> GetProfile(string? username, ClaimsPrincipal user, int page)
		{

			ProfileViewModel viewModel = new ProfileViewModel();
			if (page < 1) page = 1;

			if ((username is null && user.Identity.IsAuthenticated) || user.Identity.Name == username)
			{

				viewModel.Name = user.Identity.Name;

				viewModel.IsOwner = true;
			}

			else if (await IsExistByNameAsync(username))
			{
				viewModel.Name = username;
				viewModel.IsOwner = false;
			}
			else
			{
				return null;
			}

			viewModel.posts = await postRepository
				.GetAll()
				.Where(e => e.User.UserName == viewModel.Name)
				.OrderByDescending(e => e.CreatedAt)
				.Select(PostProjection.ToPostCardViewModel())
				.ToPagedListAsync(page, PagedConstant.Size);

			return viewModel;
		}



		//public async Task<IPagedList<PostCardViewModel>> GetPostsOfUserByIdAsync(string userId, int page)
		//{
		//	var size = 20;

		//	var posts = userManager.Users.Where(e => e.Id == userId).SelectMany(e => e.Posts);

		//	var viewModel = posts
		//		.AsNoTracking()
		//		.Include(e => e.Community)
		//		.OrderByDescending(e => e.CreatedAt)
		//		.Select(e => new PostCardViewModel
		//		{
		//			AuthorName = null, // it is null tihs is used in Account not make sessns to show username with post
		//			AuthorId = null,
		//			CommunityName = e.Community.Name,
		//			CommunityId = e.CommunityId,
		//			Title = e.Title,
		//			CreatedAt = e.CreatedAt,
		//			Id = e.Id

		//		});
		//	return await viewModel.ToPagedListAsync(page, size);
		//}



		public async Task<bool> IsExistByNameAsync(string userName)
		{
			return await userManager.Users.AnyAsync(e => e.UserName == userName);
		}

	}
}
