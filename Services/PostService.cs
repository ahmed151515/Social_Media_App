using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Services;

public class PostService(IUnitOfWork unitOfWork) : IService<Post>
{
	public async Task<IEnumerable<Post>> GetAllAsync()
	{
		return await unitOfWork.PostRepository.GetAll()
			.ToListAsync();
	}

	public async Task<IEnumerable<Post>> GetAllWithIncludesAsync()
	{
		return await unitOfWork.PostRepository.GetAllWithIncludes().ToListAsync();
	}

	public async Task<IEnumerable<Post>> PaginateAsync(int page = 1, int pageSize = 20)
	{
		// the check of page and pagesize is in Repository

		return await unitOfWork.PostRepository.Paginate(page, pageSize).ToListAsync();
	}

	public async Task<IEnumerable<Post>> PaginateWithIncludeAsync(int page = 1, int pageSize = 20)
	{
		return await unitOfWork.PostRepository.PaginateWithIncludes(page, pageSize).ToListAsync();
	}

	public async Task<Post?> GetByIdAsync(int id)
	{
		return await unitOfWork.PostRepository.GetByIdAsync(id);
	}

	public async Task<Post?> GetByIdWithIncludeAsync(int id)
	{
		return await unitOfWork.PostRepository.GetAllWithIncludes()
			.SingleOrDefaultAsync(c => c.Id == id);
	}

	public async Task<IEnumerable<Post>> FindAsync(
		Expression<Func<Post, bool>> predicate)
	{
		return await unitOfWork.PostRepository.GetAll().Where(predicate)
			.ToListAsync();
	}
	public async Task<IEnumerable<Post>> FindWithIncludeAsync(
		Expression<Func<Post, bool>> predicate)
	{
		return await unitOfWork.PostRepository.GetAllWithIncludes().Where(predicate)
			.ToListAsync();
	}


	public async Task CreateAsync(Post newPost)
	{
		await unitOfWork.PostRepository.AddAsync(newPost);

		await unitOfWork.SaveChangeAsync();


	}

	public async Task UpdateAsync(int id, Post postToUpdate)
	{
		ArgumentNullException.ThrowIfNull(postToUpdate);

		var oldPost = await unitOfWork.PostRepository.GetByIdAsync(id);

		if (oldPost is null)
		{
			throw new ArgumentException("Id not found");
		}

		oldPost.Title = postToUpdate.Title;
		oldPost.Content = postToUpdate.Content;

		await unitOfWork.SaveChangeAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var post = await unitOfWork.PostRepository.GetByIdAsync(id);

		if (post is null)
		{
			throw new ArgumentException("Id not found");
		}

		unitOfWork.PostRepository.Delete(post);

		await unitOfWork.SaveChangeAsync();
	}


}