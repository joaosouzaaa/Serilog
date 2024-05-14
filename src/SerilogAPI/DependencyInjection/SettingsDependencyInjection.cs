using SerilogAPI.Filters;
using SerilogAPI.Interfaces.Settings;
using SerilogAPI.Settings.NotificationSettings;

namespace SerilogAPI.DependencyInjection;

internal static class SettingsDependencyInjection
{
    internal static void AddSettingsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler, NotificationHandler>();

        services.AddScoped<NotificationFilter>();

        services.AddMvc(options => options.Filters.AddService<NotificationFilter>());
    }
}
