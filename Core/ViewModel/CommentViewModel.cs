using System.ComponentModel.DataAnnotations;

namespace Core.ViewModel
{
	public class CommentViewModel
	{

		public int Id { get; set; }


		[Required][MaxLength(300)] public string Content { get; set; }

		public DateTime CreatedAt { get; set; }


		public int CountOfReplies { get; set; }

		public string UserId { get; set; }

		public string Username { get; set; }


		public int PostId { get; set; }

		public int? ParentCommentId { get; set; }
		public string? ParentCommentUsername { get; set; }

	}
}
