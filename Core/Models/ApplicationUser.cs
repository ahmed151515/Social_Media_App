using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class ApplicationUser : IdentityUser, ISoftDeleteable
{
	[Required]
	public override string Email { get => base.Email; set => base.Email = value; }
	[Required]
	public override string UserName { get => base.UserName; set => base.UserName = value; }


	public ICollection<Community> Communities { get; set; } =
		new List<Community>();

	public ICollection<Membership> Memberships { get; set; } =
		new List<Membership>();

	public ICollection<Post> Posts { get; set; } = new List<Post>();
	public ICollection<Comment> Comments { get; set; } = new List<Comment>();


	public bool IsDeleted { get; set; } = false;
	public DateTime? DeleteDate { get; set; }

	public void Delete()
	{
		IsDeleted = true;
		DeleteDate = DateTime.UtcNow;

		var newId = $"Deleted_{Id}";

		UserName = newId;
		NormalizedUserName = UserName.ToUpper();
		Email = $"{newId}@anonymous.com";
		NormalizedEmail = Email.ToUpper();

		PasswordHash = null;
		SecurityStamp = Guid.NewGuid().ToString();



	}
}