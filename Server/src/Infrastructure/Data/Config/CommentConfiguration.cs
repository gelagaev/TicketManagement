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
    builder.Property(t => t.CommentText)
      .IsRequired()
      .HasMaxLength(1000);
    builder.Property(t => t.UserId)
      .IsRequired();
  }
}
