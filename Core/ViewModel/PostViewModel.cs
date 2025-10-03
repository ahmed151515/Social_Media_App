using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Core.ViewModel
{
	public class PostViewModel
	{
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }
		[HiddenInput(DisplayValue = false)]
		public int CommunityId { get; set; }

		[Required]
		[MinLength(5)]
		[MaxLength(30)]
		public string Title { get; set; }

		[Required]
		[MaxLength(2000)]
		public string Content { get; set; }
	}
}
