using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Models;
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


}