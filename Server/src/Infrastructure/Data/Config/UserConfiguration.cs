using Core.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(user => user.Id);
    builder.Property(entity => entity.Id).HasDefaultValueSql("newsequentialid()");
    builder.Property(user => user.IsActive).IsRequired().HasDefaultValue(false);
  }
}
