using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

public class PostConfig : IEntityTypeConfiguration<Post>
{
	public void Configure(EntityTypeBuilder<Post> builder)
	{
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Id).ValueGeneratedOnAdd();

		//builder.Property(e => e.Title).HasColumnType("NVARCHAR")
		//	.HasMaxLength(30);
		//builder.Property(e => e.Content).HasColumnType("NVARCHAR")
		//	.HasMaxLength(2000);
		builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");


		builder.HasOne(e => e.User).WithMany(e => e.Posts)
			.HasForeignKey(e => e.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasOne(e => e.Community).WithMany(e => e.Posts)
			.HasForeignKey(e => e.CommunityId).IsRequired()
			.OnDelete(DeleteBehavior.Cascade);
	}
}