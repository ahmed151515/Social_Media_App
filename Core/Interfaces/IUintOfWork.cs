using Core.Interfaces.Repository;

namespace Core.Interfaces;

public interface IUnitOfWork
{

	public IPostRepository PostRepository { get; }
	public ICommentRepository CommentRepository { get; }
	public ICommunityRepository CommunityRepository { get; }
	public IMembershipRepository MembershipRepository { get; }

	public Task<int> SaveChangeAsync();
	public Task BeginTransactionAsync();
	public Task CommitAsync();
	public Task RollbackAsync();
}