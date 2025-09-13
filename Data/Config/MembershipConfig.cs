using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

public class MembershipConfig : IEntityTypeConfiguration<Membership>
{
	public void Configure(EntityTypeBuilder<Membership> builder)
	{
		builder.HasKey(e => new { e.CommunityId, e.UserId });

		builder.Property(e => e.CommunityRole).HasConversion<string>().HasMaxLength(30);

		builder.HasQueryFilter(e => e.Community.IsDeleted != true);
		builder.HasQueryFilter(e => e.User.IsDeleted != true);
	}
}