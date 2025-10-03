using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Models;
using Core.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X.PagedList;
using X.PagedList.EF;

namespace Services;

public class PostService(IUnitOfWork unitOfWork) : IPostService
{
	public async Task<IEnumerable<Post>> GetAllAsync()
	{
		return await unitOfWork.PostRepository.GetAll()
			.ToListAsync();
	}





	public async Task<Post?> GetByIdAsync(int id)
	{
		return await unitOfWork.PostRepository.GetByIdAsync(id);
	}



	public async Task<IEnumerable<Post>> FindAsync(
		Expression<Func<Post, bool>> predicate)
	{
		return await unitOfWork.PostRepository.GetAll().Where(predicate)
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

	public async Task<IPagedList<Post>> PagingAsync(int page)
	{
		if (page < 1) page = 1;
		var size = 20;

		return await unitOfWork.PostRepository.GetAll().OrderByDescending(e => e.CreatedAt).ToPagedListAsync(page, size);

	}




	public async Task<PostDetailsViewModel?> GetPostDetailsAsync(int id)
	{
		var post = await unitOfWork.PostRepository
			.GetAll()

			.Where(e => e.Id == id)
			.Select(e => new PostDetailsViewModel
			{
				Id = e.Id,
				Title = e.Title,
				Content = e.Content,
				User = e.User,
				Community = e.Community,
				CommunityId = e.CommunityId,
				CreatedAt = e.CreatedAt,
				Comments = e.Comments
				.Where(c => c.ParentCommentId == null)
				.Select(c => new CommentViewModel
				{
					Id = c.Id,
					Content = c.Content,
					CreatedAt = c.CreatedAt,
					Username = c.User.UserName,
					CountOfReplies = c.Replies.Count()
				}).ToList()
			}).SingleOrDefaultAsync();



		return post;


	}

	public bool IsOwner(Post post, string userId)
	{
		return userId == post.UserId;
	}
	public async Task<bool> IsOwnerAsync(int id, string userId)
	{


		return await unitOfWork.PostRepository.GetAll().AnyAsync(e => e.UserId == userId && e.Id == id);
	}

	public async Task<int> CreateAsync(PostViewModel post, string userId)
	{
		var newPost = new Post
		{
			Title = post.Title,
			Content = post.Content,
			UserId = userId,
			CommunityId = post.CommunityId
		};

		await unitOfWork.PostRepository.AddAsync(newPost);
		await unitOfWork.SaveChangeAsync();

		return newPost.Id;
	}



	public Task<Post?> GetByIdAndUserIdAsync(int id, string userId)
	{
		return unitOfWork.PostRepository.GetAll().SingleOrDefaultAsync(e => e.Id == id && e.UserId == userId);
	}

	public async Task<int> UpdateAsync(PostViewModel post)
	{
		var oldPost = await unitOfWork.PostRepository.GetByIdAsync(post.Id);

		oldPost.Title = post.Title;
		oldPost.Content = post.Content;

		await unitOfWork.SaveChangeAsync();

		return post.Id;
	}


}