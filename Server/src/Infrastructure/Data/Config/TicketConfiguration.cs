using Core.TicketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
  public void Configure(EntityTypeBuilder<Ticket> builder)
  {
    builder.HasKey(user => user.Id);
    builder.Property(entity => entity.Id).HasDefaultValueSql("newsequentialid()");

    builder.Property(p => p.Subject)
      .HasMaxLength(100)
      .IsRequired();

    builder.Property(p => p.Description)
      .HasMaxLength(1000)
      .IsRequired();

    builder.Property(p => p.Priority)
      .HasConversion(
        p => p.Value,
        p => PriorityStatus.FromValue(p));

    builder.Property(t => t.AssignedId)
    .IsRequired(false);


    builder.HasOne(t => t.AssignedTo)
      .WithMany()
      .HasForeignKey(t => t.AssignedId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(t => t.Author)
      .WithMany()
      .HasForeignKey(t => t.AuthorId)
      .OnDelete(DeleteBehavior.Cascade);;

    builder.ToTable("Tickets", "dbo");
  }
}
