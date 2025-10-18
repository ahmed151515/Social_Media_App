using System.Security.Claims;

namespace Web.Extension
{
	public static class ClaimsPrincipalExtension
	{

		public static string? GetUserId(this ClaimsPrincipal User)
		{
			return User.Claims.SingleOrDefault(e => e.Type == ClaimTypes.NameIdentifier).Value;
		}
	}
}
