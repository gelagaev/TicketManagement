using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class RoleClimeConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
{
  public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> builder)
  {
  }
}
