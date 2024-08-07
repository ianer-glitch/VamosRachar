using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Configuration;

namespace ServiceBusServer;

public static class ServiceBusConections
{

    public static void  SendObjectOnNotiftyQueue(IConfiguration conf, object request)
    {
        string queueName = conf.GetSection("ServiceBusSettings").GetSection("notifyQueue").Value ?? throw new Exception("notify queue not found");
        SendObjectOnQueue(request,queueName);
    }

    public static void ListeningObjectsInNotificationQueue<T>(Action<T> functionToRun, CancellationToken cancelToken,
        IConfiguration conf)
    {
        string queueName = conf.GetSection("ServiceBusSettings").GetSection("notifyQueue").Value ?? throw new Exception("notify queue not found");
        ListeningObjectsInQueue<T>(functionToRun, cancelToken, queueName);
    }
    
    private static ConnectionFactory GetConnectionFactory()
    {
        var factory = new ConnectionFactory
        {
            UserName = "guest",
            Password = "guest",
            Port = 5672,
            HostName = "service-bus"
        };
        return factory;
    }
    
    public static void  SendObjectOnQueue(object request, string queueName)
    {
        ConnectionFactory factory = GetConnectionFactory();
        using var connection =  factory.CreateConnection();
        using var channel =  connection.CreateModel();

        channel.QueueDeclare(queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        string requestMessage = JsonSerializer.Serialize(request);
        var body = Encoding.UTF8.GetBytes(requestMessage);
            
        channel.BasicPublish(exchange: string.Empty,
            routingKey: queueName,
            basicProperties: null,
            body: body);
    }
    public static void ListeningObjectsInQueue<T>(Action<T> functionToRun, CancellationToken cancelToken, string queueName)
    {
        
        
        
        ConnectionFactory factory = GetConnectionFactory();
        using var connection =  factory.CreateConnection();
        using var channel =  connection.CreateModel();

        channel.QueueDeclare(queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

    
        while (!cancelToken.IsCancellationRequested)
        {
            var consumer = new EventingBasicConsumer(channel);
            
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                var objectFromQueue = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(message);
                functionToRun(objectFromQueue);
            };

            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);
        }
        
    }

}