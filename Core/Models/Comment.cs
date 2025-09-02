namespace Core.Models;

public class Comment
{
	public int Id { get; set; }

	public string Content { get; set; }
	public DateTime CreatedAt { get; set; }

	public ApplicationUser User { get; set; }
	public string UserId { get; set; }
	public Post Post { get; set; }
	public int PostId { get; set; }

	public Comment? CommentReply { get; set; }
	public int? CommentReplyId { get; set; }
}