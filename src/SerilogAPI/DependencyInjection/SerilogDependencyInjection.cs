using Serilog;
using System.Reflection;

namespace SerilogAPI.DependencyInjection;

internal static class SerilogDependencyInjection
{
    internal static void AddSerilogDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog(loggerConfiguration =>
            loggerConfiguration.ReadFrom.Configuration(configuration));

        //services.AddSerilog(loggerConfiguration =>
        //    loggerConfiguration
        //        .MinimumLevel.Information()
        //        .WriteTo.Console()
        //        .WriteTo.File(
        //            configuration["Serilog:WriteTo:1:Args:path"]!,
        //            rollingInterval: RollingInterval.Day,
        //            rollOnFileSizeLimit: true)
        //        .Enrich.WithThreadId()
        //        .Enrich.WithClientIp()
        //        .Enrich.WithCorrelationId()
        //        .Enrich.WithProcessId()
        //        .Enrich.FromLogContext());
    }
}
