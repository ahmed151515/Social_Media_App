using Core.Models;
using Core.ViewModel;

namespace Core.Interfaces.Services;

public interface ICommunityService : IBasicService<Community>
{
	Task CreateAsync(Community newCommunity, string userId);
	Task<CommunityDetailsViewModel> GetCommunityDetailsAsync(int id, int page);
}


