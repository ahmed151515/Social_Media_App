namespace Core.Models;

public class Community
{
	public int Id { get; set; }

	public string Name { get; set; }
	public string? Description { get; set; }
	public int Followers { get; set; }

	public ICollection<ApplicationUser> Users { get; set; } =
		new List<ApplicationUser>();

	public ICollection<Post> Posts { get; set; } = new List<Post>();
}