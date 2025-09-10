using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

public class CommunityConfig : IEntityTypeConfiguration<Community>
{
	public void Configure(EntityTypeBuilder<Community> builder)
	{
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Id).ValueGeneratedOnAdd();

		//builder.Property(e => e.Name).HasColumnType("NVARCHAR")
		//	.HasMaxLength(30);
		//builder.Property(e => e.Description).HasColumnType("NVARCHAR")
		//	.HasMaxLength(255);


		builder.HasMany(e => e.Users).WithMany(e => e.Communities)
			.UsingEntity<Membership>(r =>
					r.HasOne(e => e.User).WithMany(e => e.Memberships)
						.HasForeignKey(e => e.UserId)
						.OnDelete(DeleteBehavior.Cascade),
				// Left
				l => l
					.HasOne(e => e.Community)
					.WithMany(e => e.Memberships)
					.HasForeignKey(e => e.CommunityId)
					.OnDelete(DeleteBehavior.Cascade)
			);

		builder.HasQueryFilter(c => c.IsDeleted != true);

	}
}