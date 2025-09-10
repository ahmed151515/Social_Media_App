using Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Community : ISoftDeleteable
{
	public int Id { get; set; }

	[Required]
	[MinLength(5)]
	[MaxLength(15)]
	public string Name { get; set; }

	[Required][MaxLength(255)] public string? Description { get; set; }


	public ICollection<ApplicationUser> Users { get; set; } =
		new List<ApplicationUser>();

	public ICollection<Membership> Memberships { get; set; } =
		new List<Membership>();

	public ICollection<Post> Posts { get; set; } = new List<Post>();

	public bool IsDeleted { get; set; } = false;
	public DateTime? DeleteDate { get; set; }
	public void Delete()
	{
		IsDeleted = true;
		DeleteDate = DateTime.UtcNow;

		Name = $"[Archived] {Name}";
	}
}