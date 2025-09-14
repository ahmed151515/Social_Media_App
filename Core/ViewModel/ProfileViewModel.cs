using X.PagedList;

namespace Core.ViewModel
{
	public class ProfileViewModel
	{
		public string Name { get; set; }
		public bool IsOwner { get; set; }

		public IPagedList<PostCardViewModel> posts { get; set; }
	}
}
