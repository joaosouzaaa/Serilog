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
        //            path: configuration["Serilog:WriteTo:1:Args:path"]!,
        //            rollingInterval: RollingInterval.Day,
        //            rollOnFileSizeLimit: true)
        //        .WriteTo.Seq(serverUrl: configuration["Serilog:WriteTo:2:Args:serverUrl"]!)
        //        .Enrich.WithThreadId()
        //        .Enrich.WithClientIp()
        //        .Enrich.WithCorrelationId()
        //        .Enrich.WithProcessId()
        //        .Enrich.FromLogContext());
    }
}
