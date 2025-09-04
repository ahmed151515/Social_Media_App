using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Comment
{
	public int Id { get; set; }

	[Required] [MaxLength(300)] public string Content { get; set; }

	// default value in Config
	public DateTime CreatedAt { get; set; }

	public ApplicationUser User { get; set; }
	public string UserId { get; set; }
	public Post Post { get; set; }
	public int PostId { get; set; }

	public Comment? ParentComment { get; set; }
	public int? ParentCommentId { get; set; }

	public ICollection<Comment> Replies { get; set; } = new List<Comment>();
}