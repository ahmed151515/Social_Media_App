using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
	public class ApplicationUser : IdentityUser
	{
	}

	public class Community
	{
		[Required]
		[MinLength(3)]
		[MaxLength(30)]
		public string Name { get; set; }
		public string? Description { get; set; }
		public int Followers { get; set; }

	}
	public class Post
	{
		[Required]
		public string Title { get; set; }
		[Required]
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; }


	}
	public class Commnet
	{

		[Required]
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; }

	}
}
