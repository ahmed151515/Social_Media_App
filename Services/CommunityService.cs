using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Models;
using Data.Extension;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Services;

public class CommunityService(IUnitOfWork unitOfWork) : ICommunityService
{



	public async Task<IEnumerable<Community>> GetAllAsync()
	{
		return await unitOfWork.CommunityRepository.GetAll()
			.ToListAsync();
	}

	public async Task<IEnumerable<Community>> GetAllWithIncludesAsync()
	{
		return await unitOfWork.CommunityRepository.GetAllWithIncludes().ToListAsync();
	}



	public async Task<Community?> GetByIdAsync(int id)
	{
		return await unitOfWork.CommunityRepository.GetByIdAsync(id);
	}

	public async Task<Community?> GetByIdWithIncludeAsync(int id)
	{
		return await unitOfWork.CommunityRepository.GetAllWithIncludes()
			.SingleOrDefaultAsync(c => c.Id == id);
	}

	public async Task<IEnumerable<Community>> FindAsync(
		Expression<Func<Community, bool>> predicate)
	{
		return await unitOfWork.CommunityRepository.GetAll().Where(predicate)
			.ToListAsync();
	}
	public async Task<IEnumerable<Community>> FindWithIncludeAsync(
		Expression<Func<Community, bool>> predicate)
	{
		return await unitOfWork.CommunityRepository.GetAllWithIncludes().Where(predicate)
			.ToListAsync();
	}


	public async Task CreateAsync(Community newCommunity)
	{
		await unitOfWork.CommunityRepository.AddAsync(newCommunity);

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

	public async Task<IEnumerable<Community>> PagingAsync(int page, int size)
	{

		return await unitOfWork.CommunityRepository.GetAll().PaginateAsync(page, size);

	}

	public async Task<int> CountAsync()
	{
		return await unitOfWork.CommunityRepository.GetAll().CountAsync();
	}
}