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
        //    new LoggerConfiguration()
        //        .MinimumLevel.Debug()
        //        .WriteTo.Console()
        //        .WriteTo.File(
        //            configuration["Serilog:WriteTo:Args:path"]!,
        //            rollingInterval: RollingInterval.Day,
        //            rollOnFileSizeLimit: true)
        //        .Enrich.WithThreadId()
        //        .Enrich.WithClientIp()
        //        .Enrich.WithCorrelationId()
        //        .Enrich.WithProcessId()
        //        .Enrich.FromLogContext());
    }
}
