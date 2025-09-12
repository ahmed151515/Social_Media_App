using Core.Interfaces.Repository;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CommentRepository(AppDbContext context) : ICommentRepository
{
	public IQueryable<Comment> GetAll()
	{
		return context.Comments.AsNoTracking();
	}

	public IQueryable<Comment> GetAllWithIncludes()
	{
		return GetAll()
			.Include(c => c.ParentComment)
			.Include(c => c.Post)
			.Include(c => c.User)
			.Include(c => c.Replies)
			.AsNoTracking();
	}

	public async Task<Comment?> GetByIdAsync(int id)
	{
		var comment =
			await context.Comments.SingleOrDefaultAsync(p => p.Id == id);

		return comment;
	}

	public async Task AddAsync(Comment comment)
	{
		ArgumentNullException.ThrowIfNull(comment);

		await context.Comments.AddAsync(comment);
	}

	public void Delete(Comment comment)
	{
		ArgumentNullException.ThrowIfNull(comment);
		context.Comments.Remove(comment);
	}



	public void Update(Comment comment)
	{
		ArgumentNullException.ThrowIfNull(comment);

		context.Comments.Update(comment);
	}
}