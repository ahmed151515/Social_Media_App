using Microsoft.AspNetCore.Identity;

namespace Core.Models;

public class ApplicationUser : IdentityUser
{
	public ICollection<Community> Communities { get; set; } =
		new List<Community>();

	public ICollection<Membership> Memberships { get; set; } =
		new List<Membership>();

	public ICollection<Post> Posts { get; set; } = new List<Post>();
	public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}