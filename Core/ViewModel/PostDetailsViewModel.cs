using Core.Models;

namespace Core.ViewModel
{
	public class PostDetailsViewModel
	{
		public int Id { get; set; }


		public string Title { get; set; }

		public string Content { get; set; }

		public DateTime CreatedAt { get; set; }

		public ApplicationUser? User { get; set; }


		public Community? Community { get; set; }
		public int CommunityId { get; set; }

		public ICollection<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
	}
}
