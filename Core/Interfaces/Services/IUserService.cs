using Core.Models;

namespace Core.Interfaces.Services;

public interface IUserService
{

	Task<List<Community>> GetCommunitiesOfUserByIDAsync(string userId);
}


