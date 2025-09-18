using Core.Interfaces;
using Core.Interfaces.Repository;

namespace Data;

public class UnitOfWork(
	IPostRepository postRepository,
	ICommentRepository commentRepository,
	ICommunityRepository communityRepository,
	IMembershipRepository membershipRepository,
	AppDbContext context)
	: IUnitOfWork
{
	public IPostRepository PostRepository { get; } = postRepository;
	public ICommentRepository CommentRepository { get; } = commentRepository;

	public ICommunityRepository CommunityRepository { get; } = communityRepository;

	public IMembershipRepository MembershipRepository { get; } = membershipRepository;

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