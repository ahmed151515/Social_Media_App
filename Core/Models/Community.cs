using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Community
{
	public int Id { get; set; }

	[Required]
	[MinLength(5)]
	[MaxLength(15)]
	public string Name { get; set; }

	[Required] [MaxLength(255)] public string? Description { get; set; }


	public ICollection<ApplicationUser> Users { get; set; } =
		new List<ApplicationUser>();

	public ICollection<Membership> Memberships { get; set; } =
		new List<Membership>();

	public ICollection<Post> Posts { get; set; } = new List<Post>();
}