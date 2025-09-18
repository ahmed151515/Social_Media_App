using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Services
{
	public class MembershipService(IUnitOfWork unitOfWork) : IMembershipService
	{
		public async Task JoinAsync(Membership membership)
		{
			if (await IsJoinAsync(membership.UserId, membership.CommunityId))
			{
				return;
			}

			await unitOfWork.MembershipRepository.AddAsync(membership);

			await unitOfWork.SaveChangeAsync();
		}

		public async Task LeaveAsync(string userId, int CommunityId)
		{
			if (await IsJoinAsync(userId, CommunityId))
			{
				var membership = await unitOfWork.MembershipRepository.Memberships
				.SingleAsync(e => e.UserId == userId && e.CommunityId == CommunityId);

				unitOfWork.MembershipRepository
					.Delete(membership);

				await unitOfWork.SaveChangeAsync();
			}



		}

		public async Task<bool> IsJoinAsync(string userId, int communityId)
		{
			var membership = await unitOfWork.MembershipRepository.Memberships
				.AnyAsync(e => e.CommunityId == communityId && e.UserId == userId);

			return membership;
		}
	}
}
