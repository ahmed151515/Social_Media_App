using System.Linq.Expressions;
using Core.Models;

namespace Core.Interfaces.Repository;

public interface ICommentRepository
{
	Task<Comment?> GetByIdAsync(int id);
	Task<IEnumerable<Comment>> GetAllAsync();

	Task<IEnumerable<Comment>> FindAsync(
		Expression<Func<Comment, bool>> predicate);

	Task AddAsync(Comment comment);
	void Update(Comment comment);
	void Delete(Comment comment);
}