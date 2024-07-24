using Notify.Models;
using Notify.Repositories.NotifyRepository;

namespace Notify.Repositories.NotificationRepository;

public interface INotificationRepository : INotifyRepository<Notification>
{
    
}