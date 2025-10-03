using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Core.ViewModel
{
	public class CommentCreateEditDeleteViewModel
	{
		[HiddenInput(DisplayValue = false)]

		public int Id { get; set; }


		[Required][MaxLength(300)] public string Content { get; set; }





		[HiddenInput(DisplayValue = false)]

		public int PostId { get; set; }
		[HiddenInput(DisplayValue = false)]
		public int? ParentCommentId { get; set; }

	}
}
