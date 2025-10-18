using Core.Models;
using Core.ViewModel;
using System.Linq.Expressions;

namespace Core.Extension
{
	public static class PostProjection
	{
		public static Expression<Func<Post, PostCardViewModel>> ToPostCardViewModel() =>
			post =>
			 new PostCardViewModel
			 {
				 Id = post.Id,
				 Title = post.Title,
				 CommunityName = post.Community.Name,
				 CommunityId = post.CommunityId,
				 AuthorId = post.UserId,
				 AuthorName = post.User.UserName,
				 CreatedAt = post.CreatedAt,
				 Content = post.Content.Length > 100 ? post.Content.Substring(0, 100) + "..." : post.Content
			 };


	}
}
