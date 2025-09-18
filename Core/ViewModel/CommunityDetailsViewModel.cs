using X.PagedList;

namespace Core.ViewModel
{
	public class CommunityDetailsViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public IPagedList<PostCardViewModel> Posts { get; set; }

		public bool IsJoin { get; set; }
	}
}
