using System.Linq.Expressions;

namespace Core.Interfaces.Services;

public interface IBasicService<TEntity>
{
	Task<IEnumerable<TEntity>> GetAllAsync();
	Task<IEnumerable<TEntity>> GetAllWithIncludesAsync();

	Task<IEnumerable<TEntity>> PagingAsync(int page, int size);
	Task<int> CountAsync();
	Task<TEntity?> GetByIdAsync(int id);
	Task<TEntity?> GetByIdWithIncludeAsync(int id);

	Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
	Task<IEnumerable<TEntity>> FindWithIncludeAsync(Expression<Func<TEntity, bool>> predicate);


	Task CreateAsync(TEntity newEntity);
	Task UpdateAsync(int id, TEntity postToUpdate);
	Task DeleteAsync(int id);
}


