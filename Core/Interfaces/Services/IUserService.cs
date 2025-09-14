using Core.Models;
using Core.ViewModel;
using X.PagedList;

namespace Core.Interfaces.Services;

public interface IUserService
{

	Task<List<Community>> GetCommunitiesOfUserByIDAsync(string userId);
	Task<IPagedList<PostCardViewModel>> GetPostsOfUserByUserNameAsync(string userName, int page, int size);
	Task<IPagedList<PostCardViewModel>> GetPostsOfUserByIdAsync(string userId, int page, int size);
	ValueTask<bool> IsExistByNameAsync(string userName);
}


