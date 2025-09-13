using Core.Models;

namespace Core.Interfaces.Services;

public interface IPostService : IBasicService<Post>
{
	Task<IEnumerable<Post>> PagingWithIncludesAsync(int page, int size);
}


