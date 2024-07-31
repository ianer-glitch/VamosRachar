using System.Text;
using Grpc.Core;
using Notify.Repositories.NotificationRepository;
using Notify.Repositories.NotifyRepository;
using ProtoServer.ProtoFiles;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Notify.UseCases;

public class NotificationUseCase
{
    private readonly INotificationRepository  _notificationRepo;

    public NotificationUseCase(INotificationRepository  notificationRepo)
    {
        _notificationRepo = notificationRepo;
    }
    // public override async Task<PNotification> CreateNotification(PNotification request, ServerCallContext context)
    // {
    //     return await _notificationRepo.CreateNotification(request);
    // }

    public async Task CreateNotificationRabitMq()
    {
        // var factory = new ConnectionFactory { HostName = "amqp://guest:guest@service-bus:5672/" };
        
        var factory = new ConnectionFactory
        {
            UserName = "guest",
            Password = "guest",
            Port = 5672,
            HostName = "service-bus"
        };
        
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        
        
        
        channel.QueueDeclare(queue: "hello",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            PNotification request = new()
            {
                Message = message
            };
            Console.WriteLine("Message recived " + message);
            _notificationRepo.CreateNotification(request);
        };
        channel.BasicConsume(queue: "hello",
            autoAck: true,
            consumer: consumer);
    }
}