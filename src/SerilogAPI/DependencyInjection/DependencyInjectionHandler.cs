﻿namespace SerilogAPI.DependencyInjection;

internal static class DependencyInjectionHandler
{
    internal static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCorsDependencyInjection();
        services.AddSettingsDependencyInjection();
    }
}
