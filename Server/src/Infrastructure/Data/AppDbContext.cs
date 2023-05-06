using System.Reflection;
using Core.TicketAggregate;
using Core.UserAggregate;
using Kernel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Kernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : IdentityDbContext<User, Role, Guid,
  IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
  IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
  private readonly IDomainEventDispatcher? _dispatcher;

  public AppDbContext(DbContextOptions<AppDbContext> options,
    IDomainEventDispatcher? dispatcher)
      : base(options)
  {
    _dispatcher = dispatcher;
  }

  public override DbSet<User> Users { get; set; } = default!;
  public override DbSet<IdentityRoleClaim<Guid>> RoleClaims { get; set; } = default!;
  public override DbSet<UserRole> UserRoles { get; set; } = default!;
  public override DbSet<Role> Roles { get; set; } = default!;
  public override DbSet<IdentityUserClaim<Guid>> UserClaims { get; set; } = default!;
  public virtual DbSet<Ticket> Tickets { get; set; } = default!;
  public virtual DbSet<Comment> Comments { get; set; } = default!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken ct = new())
  {
    int result = await base.SaveChangesAsync(ct).ConfigureAwait(false);
    
    if (_dispatcher == null) return result;

    var entitiesWithEvents = ChangeTracker.Entries<EntityBase<Guid>>()
        .Select(e => e.Entity)
        .Where(e => e.DomainEvents.Any())
        .ToArray();

    await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

    return result;
  }

  public override int SaveChanges()
  {
    return SaveChangesAsync().GetAwaiter().GetResult();
  }
}
