 using Notify.Models;
using Notify.Repositories.NotifyRepository;

namespace Notify.Repositories.NotificationRepository;

public class NotificationRepository : INotificationRepository
{
    private readonly INotifyRepository<Notification> _notify;

    public NotificationRepository(INotifyRepository<Notification> notify)
    {
        _notify = notify;
    }


    public Task<T> GetAllPagingAsync<T>(int quanity, int currenctPage)
    {
        throw new NotImplementedException();
    }

    public Task<Notification> InsertOrUpdateAsync(Notification entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Notification> SetExcludedAsync(Notification entity)
    {
        return await _notify.SetExcludedAsync(entity);
    }

    public Task<Notification> SetActiveAsync(Notification entity)
    {
        throw new NotImplementedException();
    }
}