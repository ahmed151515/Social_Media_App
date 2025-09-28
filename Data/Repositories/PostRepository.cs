using Core.Interfaces.Repository;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class PostRepository(AppDbContext context) : IPostRepository
{

	public IQueryable<Post> GetAll()
	{
		return context.Posts.AsNoTracking();
	}

	public IQueryable<Post> GetAllWithIncludes()
	{
		return GetAll()
			.Include(c => c.Community)
			.Include(c => c.User)
			.Include(c => c.Comments)
			.ThenInclude(c => c.Replies)
			.AsNoTracking();
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