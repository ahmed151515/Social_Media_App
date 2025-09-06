using Core.Interfaces.Repository;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class PostRepository(AppDbContext context) : IRepository<Post>
{
	public IQueryable<Post> GetAll()
	{
		return context.Posts;
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