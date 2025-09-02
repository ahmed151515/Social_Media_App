namespace Core.Models;

public class Post
{
	public int Id { get; set; }

	public string Title { get; set; }

	public string Content { get; set; }
	public DateTime CreatedAt { get; set; }

	public ApplicationUser User { get; set; }
	public string UserId { get; set; }
	public Community Community { get; set; }
	public int CommunityId { get; set; }

	public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}