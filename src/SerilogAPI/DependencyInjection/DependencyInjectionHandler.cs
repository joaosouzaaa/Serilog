﻿using Microsoft.EntityFrameworkCore;
using SerilogAPI.Data.DatabaseContexts;
using SerilogAPI.Factories;

namespace SerilogAPI.DependencyInjection;

internal static class DependencyInjectionHandler
{
    internal static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCorsDependencyInjection();
        services.AddSerilogDependencyInjection(configuration);

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString());
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        });

        services.AddSettingsDependencyInjection();
        services.AddRepositoriesDependencyInjection();
        services.AddMappersDependencyInjection();
        services.AddServicesDependencyInjection();
    }
}
