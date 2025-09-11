namespace Core.Interfaces.Repository;

public interface IRepository<TEntity>
{
	Task<TEntity?> GetByIdAsync(int id);
	IQueryable<TEntity> GetAll();
	IQueryable<TEntity> GetAllWithIncludes();
	IQueryable<TEntity> Paginate(int page = 1, int pageSize = 20);
	IQueryable<TEntity> PaginateWithIncludes(int page = 1, int pageSize = 20);
	Task AddAsync(TEntity entity);
	void Update(TEntity entity);
	void Delete(TEntity entity);
}