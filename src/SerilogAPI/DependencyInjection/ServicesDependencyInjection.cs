using SerilogAPI.Interfaces.Services;
using SerilogAPI.Services;

namespace SerilogAPI.DependencyInjection;

internal static class ServicesDependencyInjection
{
    internal static void AddServicesDependencyInjection(this IServiceCollection services) =>
        services.AddScoped<IPersonService, PersonService>();
}
