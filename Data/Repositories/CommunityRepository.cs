using Core.Interfaces.Repository;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CommunityRepository(AppDbContext context) : ICommunityRepository
{
	public IQueryable<Community> GetAll()
	{
		return context.Communities.AsNoTracking();
	}
	public IQueryable<Community> GetAllWithIncludes()
	{
		return GetAll()
			.Include(c => c.Users)
			.Include(c => c.Memberships)
			.Include(c => c.Posts)
			.AsNoTracking();
	}



	public async Task<Community?> GetByIdAsync(int id)
	{
		var community =
			await context.Communities.SingleOrDefaultAsync(p => p.Id == id);

		return community;
	}

	public async Task AddAsync(Community community)
	{
		ArgumentNullException.ThrowIfNull(community);

		await context.Communities.AddAsync(community);
	}

	public void Delete(Community community)
	{
		ArgumentNullException.ThrowIfNull(community);

		context.Communities.Remove(community);
	}

	public void Update(Community community)
	{
		ArgumentNullException.ThrowIfNull(community);

		context.Communities.Update(community);
	}
}