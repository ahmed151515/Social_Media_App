namespace Core.Interfaces.Repository;

public interface IBasicRepository<TEntity>
{
	Task<TEntity?> GetByIdAsync(int id);
	IQueryable<TEntity> GetAll();
	IQueryable<TEntity> GetAllWithIncludes();

	Task AddAsync(TEntity entity);
	void Update(TEntity entity);
	void Delete(TEntity entity);
}
