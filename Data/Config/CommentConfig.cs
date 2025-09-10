using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

public class CommentConfig : IEntityTypeConfiguration<Comment>
{
	public void Configure(EntityTypeBuilder<Comment> builder)
	{
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Id).ValueGeneratedOnAdd();


		//builder.Property(e => e.Content).HasColumnType("NVARCHAR")
		//	.HasMaxLength(2000);
		builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");


		builder.HasOne(e => e.ParentComment).WithMany(e => e.Replies)
			.HasForeignKey(e => e.ParentCommentId)
			.IsRequired(false).OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(e => e.User).WithMany(e => e.Comments)
			.HasForeignKey(e => e.UserId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(e => e.Post).WithMany(e => e.Comments)
			.HasForeignKey(e => e.PostId).IsRequired()
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasQueryFilter(c => c.IsDeleted != true);

	}
}