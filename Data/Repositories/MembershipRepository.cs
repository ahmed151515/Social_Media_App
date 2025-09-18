using Core.Interfaces.Repository;
using Core.Models;

namespace Data.Repositories
{
	public class MembershipRepository(AppDbContext context) : IMembershipRepository
	{
		public IQueryable<Membership> Memberships { get; } = context.Memberships;

		public async Task AddAsync(Membership membership)
		{
			await context.Memberships.AddAsync(membership);
		}

		public void Delete(Membership membership)
		{
			context.Memberships.Remove(membership);
		}
	}
}
