using Core.Models;
using Core.ViewModel;
using System.Security.Claims;

namespace Core.Interfaces.Services;

public interface IUserService
{

	Task<List<Community>> GetCommunitiesOfUserByIDAsync(string userId);
	Task<ProfileViewModel?> GetProfile(string? username, ClaimsPrincipal user, int page);

	//Task<IPagedList<PostCardViewModel>> GetPostsOfUserByUserNameAsync(string userName, int page);

	//Task<IPagedList<PostCardViewModel>> GetPostsOfUserByIdAsync(string userId, int page);
	Task<bool> IsExistByNameAsync(string userName);
}


