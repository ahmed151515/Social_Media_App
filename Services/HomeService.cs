using Core.Interfaces;
using Core.Interfaces.Services;
using Core.ViewModel;
using X.PagedList;
using X.PagedList.EF;

namespace Services
{
	public class HomeService(IUnitOfWork unitOfWork) : IHomeService
	{
		public async Task<IPagedList<PostCardViewModel>> GetFeedAsync(int page)
		{
			if (page < 1) page = 1;
			var posts = await unitOfWork.PostRepository
				.GetAll()
				.OrderByDescending(e => e.CreatedAt)
				.Select(e => new PostCardViewModel
				{
					Id = e.Id,
					Title = e.Title,
					Content = e.Content,
					AuthorName = e.User.UserName,
					AuthorId = e.UserId,
					CommunityName = e.Community.Name,
					CommunityId = e.CommunityId,
					CreatedAt = e.CreatedAt
				}).ToPagedListAsync(page, 20);

			return posts;
		}
	}
}
