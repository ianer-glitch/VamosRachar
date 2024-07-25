using Notify.Models;
using Notify.Repositories.NotifyRepository;
using ProtoServer.ProtoFiles;

namespace Notify.Repositories.NotificationRepository;

public interface INotificationRepository
{
    Task<PNotification> CreateNotification(PNotification request);
}