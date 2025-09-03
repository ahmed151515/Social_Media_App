using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

public class MembershipConfig : IEntityTypeConfiguration<Membership>
{
	public void Configure(EntityTypeBuilder<Membership> builder)
	{
		builder.HasKey(e => new { e.CommunityId, e.UserId });
	}
}