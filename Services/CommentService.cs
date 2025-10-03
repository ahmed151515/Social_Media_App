using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Models;
using Core.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X.PagedList;
using X.PagedList.EF;

namespace Services;

public class CommentService(IUnitOfWork unitOfWork) : ICommentService
{
	public async Task<IEnumerable<Comment>> GetAllAsync()
	{
		return await unitOfWork.CommentRepository.GetAll()
			.ToListAsync();
	}





	public async Task<Comment?> GetByIdAsync(int id)
	{
		return await unitOfWork.CommentRepository.GetByIdAsync(id);
	}



	public async Task<IEnumerable<Comment>> FindAsync(
		Expression<Func<Comment, bool>> predicate)
	{
		return await unitOfWork.CommentRepository.GetAll().Where(predicate)
			.ToListAsync();
	}



	public async Task CreateAsync(Comment newComment)
	{
		await unitOfWork.CommentRepository.AddAsync(newComment);

		await unitOfWork.SaveChangeAsync();
	}

	public async Task UpdateAsync(int id, Comment commentToUpdate)
	{
		ArgumentNullException.ThrowIfNull(commentToUpdate);

		var oldComment = await unitOfWork.CommentRepository.GetByIdAsync(id);

		if (oldComment is null)
		{
			throw new ArgumentException("Id not found");
		}

		oldComment.Content = commentToUpdate.Content;


		await unitOfWork.SaveChangeAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var comment = await unitOfWork.CommentRepository.GetByIdAsync(id);

		if (comment is null)
		{
			throw new ArgumentException("Id not found");
		}


		unitOfWork.CommentRepository.Delete(comment);

		await unitOfWork.SaveChangeAsync();
	}

	public async Task<IPagedList<Comment>> PagingAsync(int page)
	{

		if (page < 1) page = 1;
		var size = 20;
		return await unitOfWork.CommentRepository.GetAll().OrderByDescending(e => e.CreatedAt).ToPagedListAsync(page, size);

	}

	public async Task<List<CommentViewModel>> GetCommentRepliesAsync(int id)
	{
		var replies = await unitOfWork.CommentRepository
			.GetAll()

			.Where(e => e.Id == id)
			.SelectMany(e => e.Replies)
			.Select(e => new CommentViewModel
			{
				Id = e.Id,
				Content = e.Content,
				CreatedAt = e.CreatedAt,
				ParentCommentId = e.ParentCommentId,
				PostId = e.PostId,
				UserId = e.UserId,
				Username = e.User.UserName,
				ParentCommentUsername = e.ParentComment.User.UserName,
				CountOfReplies = e.Replies.Count()

			})
			.ToListAsync();

		return replies;
	}

	public async Task CreateAsync(CommentCreateEditDeleteViewModel comment, string userId)
	{

		var newComment = new Comment
		{
			Content = comment.Content,
			UserId = userId,
			PostId = comment.PostId,
			ParentCommentId = comment.ParentCommentId,

		};

		await unitOfWork.CommentRepository.AddAsync(newComment);

		await unitOfWork.SaveChangeAsync();
	}

	public async Task<Comment?> GetByIdAndUserIdAsync(int id, string userId)
	{
		var comment = await unitOfWork.CommentRepository.GetAll()
			.Where(e => e.Id == id && e.UserId == userId)
			.SingleOrDefaultAsync();


		return comment;
	}
	public async Task<bool> IsOwnerAsync(int id, string userId)
	{
		return await unitOfWork.CommentRepository.GetAll().AnyAsync(e => e.Id == id && e.UserId == userId);
	}

	public async Task Update(CommentCreateEditDeleteViewModel comment)
	{
		var oldCommnet = await unitOfWork.CommentRepository.GetByIdAsync(comment.Id);

		oldCommnet.Content = comment.Content;

		await unitOfWork.SaveChangeAsync();
	}

	public async Task Delete(CommentCreateEditDeleteViewModel commentData)
	{
		var commnet = await unitOfWork.CommentRepository.GetByIdAsync(commentData.Id);

		unitOfWork.CommentRepository.Delete(commnet);

		await unitOfWork.SaveChangeAsync();
	}
}