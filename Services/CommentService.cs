using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Services;

public class CommentService(IUnitOfWork unitOfWork) : IService<Comment>
{
	public async Task<IEnumerable<Comment>> GetAllAsync()
	{
		return await unitOfWork.CommentRepository.GetAll()
			.ToListAsync();
	}

	public async Task<IEnumerable<Comment>> GetAllWithIncludesAsync()
	{
		return await unitOfWork.CommentRepository.GetAllWithIncludes().ToListAsync();
	}

	public async Task<IEnumerable<Comment>> PaginateAsync(int page = 1, int pageSize = 20)
	{
		// the check of page and pagesize is in Repository

		return await unitOfWork.CommentRepository.Paginate(page, pageSize).ToListAsync();
	}

	public async Task<IEnumerable<Comment>> PaginateWithIncludeAsync(int page = 1, int pageSize = 20)
	{
		return await unitOfWork.CommentRepository.PaginateWithIncludes(page, pageSize).ToListAsync();
	}

	public async Task<Comment?> GetByIdAsync(int id)
	{
		return await unitOfWork.CommentRepository.GetByIdAsync(id);
	}

	public async Task<Comment?> GetByIdWithIncludeAsync(int id)
	{
		return await unitOfWork.CommentRepository.GetAllWithIncludes()
			.SingleOrDefaultAsync(c => c.Id == id);
	}

	public async Task<IEnumerable<Comment>> FindAsync(
		Expression<Func<Comment, bool>> predicate)
	{
		return await unitOfWork.CommentRepository.GetAll().Where(predicate)
			.ToListAsync();
	}
	public async Task<IEnumerable<Comment>> FindWithIncludeAsync(
		Expression<Func<Comment, bool>> predicate)
	{
		return await unitOfWork.CommentRepository.GetAllWithIncludes().Where(predicate)
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
}