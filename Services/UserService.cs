using Core.Interfaces.Services;
using Core.Models;
using Core.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.EF;
using X.PagedList.Extensions;

namespace Services
{
	public class UserService(UserManager<ApplicationUser> userManager) : IUserService
	{
		public async Task<List<Community>> GetCommunitiesOfUserByIDAsync(string userId)
		{

			var Communities = await userManager.Users.Where(u => u.Id == userId).SelectMany(u => u.Communities).ToListAsync();

			return Communities;
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
		public async Task<IPagedList<PostCardViewModel>> GetPostsOfUserByUserNameAsync(string userName, int page)
		{
			var size = 20;

			var posts = userManager.Users.Where(e => e.UserName == userName).SelectMany(e => e.Posts);

			var viewModel = posts
				.AsNoTracking()
				.Include(e => e.Community)
				.OrderByDescending(e => e.CreatedAt)
				.Select(e => new PostCardViewModel
				{
					AuthorName = null, // it is null tihs is used in Account not make sessns to show username with post
					AuthorId = null,
					CommunityName = e.Community.Name,
					CommunityId = e.CommunityId,
					Title = e.Title,
					CreatedAt = e.CreatedAt,
					Id = e.Id

				});
			return await viewModel.ToPagedListAsync(page, size);
		}


		public async Task<bool> IsExistByNameAsync(string userName)
		{
			return await userManager.Users.AnyAsync(e => e.UserName == userName);
		}

	}
}
