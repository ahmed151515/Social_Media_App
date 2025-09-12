

using Microsoft.EntityFrameworkCore;

namespace Data.Extension
{
	public static class IQueryableExtension
	{
		public static async Task<List<T>> PaginateAsync<T>(this IQueryable<T> source, int page = 1, int pageSize = 20)
		{
			if (page <= 0) page = 1;
			if (pageSize <= 0) pageSize = 20;

			var res = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

			return res;
		}
	}
}
