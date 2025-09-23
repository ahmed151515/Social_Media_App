using System.Linq.Expressions;
using X.PagedList;

namespace Core.Interfaces.Services;

public interface IBasicService<TEntity>
{
	Task<IEnumerable<TEntity>> GetAllAsync();


	Task<IPagedList<TEntity>> PagingAsync(int page);

	Task<TEntity?> GetByIdAsync(int id);


	Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);



	Task CreateAsync(TEntity newEntity);
	Task UpdateAsync(int id, TEntity postToUpdate);
	Task DeleteAsync(int id);
}


