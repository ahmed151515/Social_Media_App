using Core.Constants;
using Core.Extension;
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
				.Select(PostProjection.ToPostCardViewModel())
				.ToPagedListAsync(page, PagedConstant.Size);

			return posts;
		}
	}
}
