using Core.Interfaces;
using Core.Interfaces.Repository;
using Core.Models;

namespace Data;

public class UnitOfWork(
	IRepository<Post> postRepository,
	IRepository<Comment> commentRepository,
	IRepository<Community> communityRepository,
	AppDbContext context)
	: IUnitOfWork
{
	public IRepository<Post> PostRepository { get; } = postRepository;
	public IRepository<Comment> CommentRepository { get; } = commentRepository;

	public IRepository<Community> CommunityRepository { get; } =
		communityRepository;


	public async Task<int> SaveChangeAsync()
	{
		return await context.SaveChangesAsync();
	}

	public async Task BeginTransactionAsync()
	{
		await context.Database.BeginTransactionAsync();
	}

	public async Task CommitAsync()
	{
		await context.Database.CommitTransactionAsync();
	}

	public async Task RollbackAsync()
	{
		await context.Database.RollbackTransactionAsync();
	}
}