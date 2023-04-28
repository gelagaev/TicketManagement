using Core.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
  public void Configure(EntityTypeBuilder<UserRole> builder)
  {
    builder.HasKey(role => new { role.UserId, role.RoleId });
    builder.HasOne(role => role.User).WithMany(user => user.Roles)
      .HasForeignKey(role => role.UserId);
    builder.HasOne(role => role.Role).WithMany(role => role.UserRoles)
      .HasForeignKey(role => role.RoleId);
    builder.Property(role => role.UserId).HasColumnName("UserId");
    builder.Property(role => role.RoleId).HasColumnName("RoleId");
  }
}
