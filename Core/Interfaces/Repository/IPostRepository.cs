using System.Linq.Expressions;
using Core.Models;

namespace Core.Interfaces.Repository;

public interface IPostRepository
{
	Task<Post?> GetByIdAsync(int id);
	Task<IEnumerable<Post>> GetAllAsync();
	Task<IEnumerable<Post>> FindAsync(Expression<Func<Post, bool>> predicate);
	Task AddAsync(Post post);
	void Update(Post post);
	void Delete(Post post);
}