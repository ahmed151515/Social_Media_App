namespace Core.Models;

public class Membership
{
	// Composite  key config in config
	public string UserId { get; set; }
	public ApplicationUser User { get; set; }

	public int CommunityId { get; set; }
	public Community Community { get; set; }
}