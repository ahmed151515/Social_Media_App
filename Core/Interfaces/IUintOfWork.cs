using Core.Interfaces.Repository;
using Core.Models;

namespace Core.Interfaces;

public interface IUnitOfWork
{
	public IRepository<Post> PostRepository { get; }
	public IRepository<Comment> CommentRepository { get; }
	public IRepository<Community> CommunityRepository { get; }

	public Task<int> SaveChangeAsync();
	public Task BeginTransactionAsync();
	public Task CommitAsync();
	public Task RollbackAsync();
}