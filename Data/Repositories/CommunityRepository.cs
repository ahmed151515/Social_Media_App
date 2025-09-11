using Core.Interfaces.Repository;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CommunityRepository(AppDbContext context) : IRepository<Community>
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

	public IQueryable<Community> Paginate(int page = 1, int pageSize = 20)
	{
		if (page <= 0) page = 1;
		if (pageSize <= 0) pageSize = 20;

		return GetAll().Skip((page - 1) * pageSize).Take(pageSize);
	}

	public IQueryable<Community> PaginateWithIncludes(int page = 1, int pageSize = 20)
	{
		if (page <= 0) page = 1;
		if (pageSize <= 0) pageSize = 20;

		return GetAllWithIncludes().Skip((page - 1) * pageSize).Take(pageSize);
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