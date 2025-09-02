using System.Linq.Expressions;
using Core.Models;

namespace Core.Interfaces.Services;

public interface ICommunityService
{
	Task<IEnumerable<Community>> GetAllCommunitiesAsync();
	Task<Community?> GetCommunityByIdAsync(int id);

	Task<IEnumerable<Community>> FindCommunitiesAsync(
		Expression<Func<Community, bool>> predicate);

	Task CreateCommunityAsync(Community newCommunity);
	Task UpdateCommunityAsync(Community communityToUpdate);
	Task DeleteCommunityAsync(int id);
}