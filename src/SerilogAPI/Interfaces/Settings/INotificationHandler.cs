using SerilogAPI.Settings.NotificationSettings;

namespace SerilogAPI.Interfaces.Settings;

public interface INotificationHandler
{
    List<Notification> GetNotifications();
    bool HasNotifications();
    void AddNotification(string key, string message);
}
