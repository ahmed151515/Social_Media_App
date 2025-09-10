using System.Linq.Expressions;
using Core.Models;

namespace Core.Interfaces.Services;

public interface ICommentService
{
	Task<IEnumerable<Comment>> GetAllCommentsAsync();
	Task<IEnumerable<Comment>> GetAllWithIncludeAsync();
	Task<Comment?> GetCommentByIdAsync(int id);
	Task<Comment?> GetCommentByIdWithIncludeAsync(int id);

	Task<IEnumerable<Comment>> FindCommentsAsync(
		Expression<Func<Comment, bool>> predicate);


	Task CreateCommentAsync(Comment newComment);
	Task UpdateCommentAsync(int id, Comment commentToUpdate);
	Task DeleteCommentAsync(int id);
}