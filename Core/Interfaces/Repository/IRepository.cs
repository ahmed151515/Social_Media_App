namespace Core.Interfaces.Repository;

public interface IRepository<TEntity>
{
	Task<TEntity?> GetByIdAsync(int id);
	IQueryable<TEntity> GetAll();
	Task AddAsync(TEntity entity);
	void Update(TEntity entity);
	void Delete(TEntity entity);
}