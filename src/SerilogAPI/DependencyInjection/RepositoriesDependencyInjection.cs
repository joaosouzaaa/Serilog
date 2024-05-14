using SerilogAPI.Data.Repositories;
using SerilogAPI.Interfaces.Repositories;

namespace SerilogAPI.DependencyInjection;

internal static class RepositoriesDependencyInjection
{
    internal static void AddRepositoriesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPersonRepository, PersonRepository>();
    }
}
