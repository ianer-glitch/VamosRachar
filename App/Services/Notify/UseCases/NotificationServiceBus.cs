using ServiceBusServer;

namespace Notify.UseCases;

public class NotificationServiceBus : IHostedService
{
    public async  Task StartAsync(CancellationToken cancellationToken)
    {
        await ServiceBusConections.GetObjectOnQueue<string>(CreateNotificationAsync);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }


    private void CreateNotificationAsync<T>(T message)
    {
        Console.WriteLine(message);
    }
}