using System.Linq.Expressions;
using Core.Models;

namespace Core.Interfaces.Services;

public interface IPostService
{
	Task<IEnumerable<Post>> GetAllPostsAsync();
	Task<Post?> GetPostByIdAsync(int id);

	Task<IEnumerable<Post>> FindPostsAsync(
		Expression<Func<Post, bool>> predicate);

	Task CreatePostAsync(Post newPost);
	Task UpdatePostAsync(Post postToUpdate);
	Task DeletePostAsync(int id);
}