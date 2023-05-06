using Core.TicketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
  public void Configure(EntityTypeBuilder<Comment> builder)
  {
    builder.HasKey(user => user.Id);
    builder.Property(entity => entity.Id).HasDefaultValueSql("newsequentialid()");

    builder.Property(c => c.CommentText)
      .IsRequired()
      .HasMaxLength(1000);

    builder.HasOne(c => c.Author)
      .WithMany()
      .OnDelete(DeleteBehavior.Cascade);

    builder.ToTable("Comments", "dbo");
  }
}
