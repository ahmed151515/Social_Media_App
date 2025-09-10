using System.Linq.Expressions;
using Core.Models;

namespace Core.Interfaces.Services;

public interface IPostService
{
	Task<IEnumerable<Post>> GetAllPostsAsync();
	Task<IEnumerable<Post>> GetAllWithIncludeAsync();
	Task<Post?> GetPostByIdAsync(int id);
	Task<Post?> GetPostByIdWithIncludeAsync(int id);

	Task<IEnumerable<Post>> FindPostsAsync(
		Expression<Func<Post, bool>> predicate);


	Task CreatePostAsync(Post newPost);
	Task UpdatePostAsync(int id, Post postToUpdate);
	Task DeletePostAsync(int id);
}