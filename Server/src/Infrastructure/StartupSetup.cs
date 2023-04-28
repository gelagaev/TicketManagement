﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;

namespace Infrastructure;

public static class StartupSetup
{
  public static void AddDbContext(this IServiceCollection services, string connectionString) =>
    services.AddDbContext<AppDbContext>(options =>
      options.UseSqlServer(connectionString));
}
