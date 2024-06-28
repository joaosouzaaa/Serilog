using SerilogAPI.Interfaces.Mappers;
using SerilogAPI.Mappers;

namespace SerilogAPI.DependencyInjection;

internal static class MappersDependencyInjection
{
    internal static void AddMappersDependencyInjection(this IServiceCollection services) =>
        services.AddScoped<IPersonMapper, PersonMapper>();
}
