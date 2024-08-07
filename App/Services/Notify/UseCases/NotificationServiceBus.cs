using Notify.Repositories.NotificationRepository;
using ProtoServer.ProtoFiles;
using ServiceBusServer;

namespace Notify.UseCases;

public class NotificationServiceBus(INotificationRepository noti , IConfiguration conf) : IHostedService
{
    public async  Task StartAsync(CancellationToken cancellationToken)
    {
        ServiceBusConections.ListeningObjectsInNotificationQueue<string>(CreateNotificationAsync,cancellationToken,conf);
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
        noti.CreateNotification(notification);
    }

   
}