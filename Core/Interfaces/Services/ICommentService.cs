using Core.Models;
using Core.ViewModel;

namespace Core.Interfaces.Services;

public interface ICommentService : IBasicService<Comment>
{
	Task CreateAsync(CommentCreateEditDeleteViewModel comment, string userId);
	Task Delete(CommentCreateEditDeleteViewModel comment);
	Task<Comment?> GetByIdAndUserIdAsync(int id, string userId);
	Task<List<CommentViewModel>> GetCommentRepliesAsync(int id);
	Task<bool> IsOwnerAsync(int id, string userId);
	Task Update(CommentCreateEditDeleteViewModel comment);
}


