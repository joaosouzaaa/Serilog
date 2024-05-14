using Microsoft.EntityFrameworkCore;
using SerilogAPI.Data.DatabaseContexts;

namespace SerilogAPI.DependencyInjection;

internal static class MigrationHandler
{
    internal static void MigrateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        try
        {
            appDbContext.Database.Migrate();
        }
        catch
        {
            throw;
        }
    }
}
