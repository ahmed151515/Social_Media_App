using Core.Interfaces.Repository;
using Core.Models;

namespace Core.Interfaces;

public interface IUnitOfWork
{
	public IBasicRepository<Post> PostRepository { get; }
	public IBasicRepository<Comment> CommentRepository { get; }
	public IBasicRepository<Community> CommunityRepository { get; }

	public Task<int> SaveChangeAsync();
	public Task BeginTransactionAsync();
	public Task CommitAsync();
	public Task RollbackAsync();
}