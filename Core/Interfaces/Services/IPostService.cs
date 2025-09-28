using Core.Models;
using Core.ViewModel;

namespace Core.Interfaces.Services;

public interface IPostService : IBasicService<Post>
{
	Task<Post> GetPostDetails(int id);
	Task<Post?> GetByIdAndUserIdAsync(int id, string userId);
	bool IsOwner(Post post, string userId);
	Task<bool> IsOwnerAsync(int id, string userId);
	Task<int> CreateAsync(PostViewModel post, string userId);
	Task<int> UpdateAsync(PostViewModel post);

}


