using System.Linq.Expressions;
using Core.Models;

namespace Core.Interfaces.Repository;

public interface ICommunityRepository
{
	Task<Community?> GetByIdAsync(int id);
	Task<IEnumerable<Community>> GetAllAsync();

	Task<IEnumerable<Community>> FindAsync(
		Expression<Func<Community, bool>> predicate);

	Task AddAsync(Community community);
	void Update(Community community);
	void Delete(Community community);
}