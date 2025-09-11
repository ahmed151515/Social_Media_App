using Core.Interfaces.Repository;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class PostRepository(AppDbContext context) : IRepository<Post>
{

	public IQueryable<Post> GetAll()
	{
		return context.Posts.AsNoTracking();
	}

	public IQueryable<Post> GetAllWithIncludes()
	{
		return GetAll()
			.Include(c => c.Comments)
			.Include(c => c.Community)
			.AsNoTracking();
	}

	public IQueryable<Post> Paginate(int page = 1, int pageSize = 20)
	{
		if (page <= 0) page = 1;
		if (pageSize <= 0) pageSize = 20;

		return GetAll().Skip((page - 1) * pageSize).Take(pageSize);
	}

	public IQueryable<Post> PaginateWithIncludes(int page = 1, int pageSize = 20)
	{
		if (page <= 0) page = 1;
		if (pageSize <= 0) pageSize = 20;

		return GetAllWithIncludes().Skip((page - 1) * pageSize).Take(pageSize);
	}

	public async Task<Post?> GetByIdAsync(int id)
	{
		var post = await context.Posts.SingleOrDefaultAsync(p => p.Id == id);

		return post;
	}

	public async Task AddAsync(Post post)
	{
		ArgumentNullException.ThrowIfNull(post);

		await context.Posts.AddAsync(post);
	}

	public void Delete(Post post)
	{
		ArgumentNullException.ThrowIfNull(post);

		context.Posts.Remove(post);
	}

	public void Update(Post post)
	{
		ArgumentNullException.ThrowIfNull(post);

		context.Posts.Update(post);
	}


}