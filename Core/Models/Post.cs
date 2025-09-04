using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Post
{
	public int Id { get; set; }

	[Required]
	[MinLength(5)]
	[MaxLength(30)]
	public string Title { get; set; }

	[Required] [MaxLength(2000)] public string Content { get; set; }

	// default value in Config
	public DateTime CreatedAt { get; set; }

	public ApplicationUser User { get; set; }

	public string UserId { get; set; }
	public Community Community { get; set; }
	public int CommunityId { get; set; }

	public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}