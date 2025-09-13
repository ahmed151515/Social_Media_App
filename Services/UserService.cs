using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Services
{
	public class UserService(UserManager<ApplicationUser> userManager) : IUserService
	{
		public async Task<List<Community>> GetCommunitiesOfUserByIDAsync(string userId)
		{

			var Communities = await userManager.Users.Where(u => u.Id == userId).SelectMany(u => u.Communities).ToListAsync();

			return Communities;
		}
	}
}
