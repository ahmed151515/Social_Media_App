using Core.Models;

namespace Core.Interfaces.Services
{
	public interface IMembershipService
	{
		Task<bool> IsJoinAsync(string userId, int communityId);
		Task JoinAsync(Membership membership);
		Task LeaveAsync(string userId, int CommunityId);
	}
}
