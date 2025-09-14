namespace Core.ViewModel
{
	public class PostCardViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string? AuthorName { get; set; }
		public string? AuthorId { get; set; }
		public string? CommunityName { get; set; }
		public int CommunityId { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
