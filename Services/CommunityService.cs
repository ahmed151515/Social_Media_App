using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Models;
using Core.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X.PagedList;
using X.PagedList.EF;

namespace Services;

public class CommunityService(IUnitOfWork unitOfWork) : ICommunityService
{



	public async Task<IEnumerable<Community>> GetAllAsync()
	{
		return await unitOfWork.CommunityRepository.GetAll()
			.ToListAsync();
	}





	public async Task<Community?> GetByIdAsync(int id)
	{
		return await unitOfWork.CommunityRepository.GetByIdAsync(id);
	}



	public async Task<IEnumerable<Community>> FindAsync(
		Expression<Func<Community, bool>> predicate)
	{
		return await unitOfWork.CommunityRepository.GetAll().Where(predicate)
			.ToListAsync();
	}



	public async Task CreateAsync(Community newCommunity)
	{
		await unitOfWork.CommunityRepository.AddAsync(newCommunity);

		await unitOfWork.SaveChangeAsync();
	}
	public async Task CreateAsync(Community newCommunity, string userId)
	{
		await unitOfWork.CommunityRepository.AddAsync(newCommunity);

		var membership = new Membership { Community = newCommunity, UserId = userId };

		await unitOfWork.MembershipRepository.AddAsync(membership);

		await unitOfWork.SaveChangeAsync();
	}

	public async Task UpdateAsync(int id, Community communityToUpdate)
	{
		ArgumentNullException.ThrowIfNull(communityToUpdate);

		var oldCommunity = await unitOfWork.CommunityRepository.GetByIdAsync(id);

		if (oldCommunity is null)
		{
			throw new ArgumentException("Id not found");
		}

		oldCommunity.Name = communityToUpdate.Name;
		oldCommunity.Description = communityToUpdate.Description;

		await unitOfWork.SaveChangeAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var community = await unitOfWork.CommunityRepository.GetByIdAsync(id);

		if (community is null)
		{
			throw new ArgumentException("Id not found");
		}

		unitOfWork.CommunityRepository.Delete(community);

		await unitOfWork.SaveChangeAsync();
	}

	public async Task<IPagedList<Community>> PagingAsync(int page)
	{

		if (page < 1) page = 1;
		var size = 20;

		return await unitOfWork.CommunityRepository.GetAll().OrderByDescending(e => e.Id).ToPagedListAsync(page, size);

	}
	//public async Task<CommunityDetailsViewModel> GetCommunityDetails(int Id)
	//{

	//}


	public async Task<CommunityDetailsViewModel> GetCommunityDetailsAsync(int id, int page)
	{
		if (page < 1) page = 1;
		var size = 20;
		var community = await GetByIdAsync(id);
		if (community is null)
		{
			return null;
		}

		var posts = await unitOfWork.PostRepository
			.GetAll()
			.Include(e => e.User)
			.Where(e => e.CommunityId == id)
			.OrderByDescending(e => e.CreatedAt)
			.Select(e => new PostCardViewModel
			{
				Id = e.Id,
				Title = e.Title,
				CommunityName = community.Name,
				CommunityId = community.Id,
				AuthorId = e.UserId,
				AuthorName = e.User.UserName,
				CreatedAt = e.CreatedAt
			})
			.ToPagedListAsync(page, size);



		var result = new CommunityDetailsViewModel
		{
			Id = community.Id,
			Name = community.Name,
			Description = community.Description,
			Posts = posts
		};

		return result;
	}


}

