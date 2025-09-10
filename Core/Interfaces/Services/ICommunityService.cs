using System.Linq.Expressions;
using Core.Models;

namespace Core.Interfaces.Services;

public interface ICommunityService
{
	Task<IEnumerable<Community>> GetAllCommunitiesAsync();
	Task<IEnumerable<Community>> GetAllWithIncludeAsync();
	Task<Community?> GetCommunityByIdAsync(int id);
	Task<Community?> GetCommunityByIdWithIncludeAsync(int id);

	Task<IEnumerable<Community>> FindCommunitiesAsync(
		Expression<Func<Community, bool>> predicate);


	Task CreateCommunityAsync(Community newCommunity);
	Task UpdateCommunityAsync(int id, Community communityToUpdate);
	Task DeleteCommunityAsync(int id);
}