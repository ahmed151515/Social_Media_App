using Core.ViewModel;
using X.PagedList;

namespace Core.Interfaces.Services;

public interface IHomeService
{

	Task<IPagedList<PostCardViewModel>> GetFeedAsync(int page);

}


