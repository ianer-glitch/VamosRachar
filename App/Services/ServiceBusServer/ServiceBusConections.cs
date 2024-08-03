using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ServiceBusServer;

public static class ServiceBusConections
{

    private static RabbitMQ.Client.IModel  CreateSericeBusConnection(ConnectionFactory factory)
    {
        using var connection =  factory.CreateConnection();
        using var channel =  connection.CreateModel();

        channel.QueueDeclare(queue: "hello",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        return channel;

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
    private static void PublishOnServiceBus(RabbitMQ.Client.IModel channel ,Byte[] messageBody )
    {
        channel.BasicPublish(exchange: string.Empty,
            routingKey: "hello",
            basicProperties: null,
            body: messageBody);
    }
    
    public static void  SendObjectOnQueue(object request)
    {

        ConnectionFactory factory = GetConnectionFactory();
        using var connection =  factory.CreateConnection();
        using var channel =  connection.CreateModel();

        channel.QueueDeclare(queue: "hello",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        string requestMessage = JsonSerializer.Serialize(request);
        var body = Encoding.UTF8.GetBytes(requestMessage);
            
        PublishOnServiceBus(channel,body);
    }

    public static async Task ListeningObjectsInQueue<T>(Action<T> functionToRun, CancellationToken cancelToken)
    {
        
        ConnectionFactory factory = GetConnectionFactory();
        using var connection =  factory.CreateConnection();
        using var channel =  connection.CreateModel();

        channel.QueueDeclare(queue: "hello",
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

            channel.BasicConsume(queue: "hello",
                autoAck: true,
                consumer: consumer);
            }
        
    }

}