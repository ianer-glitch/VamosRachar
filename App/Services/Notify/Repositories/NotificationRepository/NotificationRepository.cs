 using Notify.Models;
using Notify.Repositories.NotifyRepository;
using ProtoServer.ProtoFiles;

namespace Notify.Repositories.NotificationRepository;

public class NotificationRepository : INotificationRepository
{
    private readonly INotifyRepository<Notification> _notify;

    public NotificationRepository(INotifyRepository<Notification> notif)
    {
        _notify = notif;
    }

    public async Task<PNotification> CreateNotification(PNotification request)
    {
        try
        {
            Notification not = new()
            {
                Changed = DateTime.Now,
                Inclusion = DateTime.Now,
                Excluded = false,
                Id = Guid.NewGuid(),
                Title = "Example Notification",
                Description = "Description Notification"
            };

            var response = await _notify.InsertAsync(not, n => n.Id == not.Id);
            if (response is null)
                throw new ArgumentNullException("Insert not ocurred");
            return request;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}