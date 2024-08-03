using Notify.Repositories.NotificationRepository;
using ProtoServer.ProtoFiles;
using ServiceBusServer;

namespace Notify.UseCases;

public class NotificationServiceBus(INotificationRepository noti) : IHostedService
{

    private readonly INotificationRepository _noti = noti;

    public async  Task StartAsync(CancellationToken cancellationToken)
    {
        await ServiceBusConections.ListeningObjectsInQueue<string>(CreateNotificationAsync,cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }


    private void CreateNotificationAsync<T>(T message)
    {
        
        PNotification notification = new()
        {
            Message = "aaaaa"
        };
        _noti.CreateNotification(notification);
    }

   
}