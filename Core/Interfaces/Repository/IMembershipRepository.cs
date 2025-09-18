using Core.Models;

namespace Core.Interfaces.Repository
{
	public interface IMembershipRepository
	{
		IQueryable<Membership> Memberships { get; }
		Task AddAsync(Membership membership);
		void Delete(Membership membership);


	}
}
