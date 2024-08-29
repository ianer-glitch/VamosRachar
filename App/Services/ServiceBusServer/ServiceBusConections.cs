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
        var notifyConfig = conf.GetSection("ServiceBusSettings").GetSection("notifyQueue");
        string queueName = notifyConfig.GetSection("QueueName").Value ?? throw new Exception("notify queue not found");
        ConnectionFactory factory = GetConnectionFactory(notifyConfig);
        SendObjectOnQueue(request,queueName,factory);
    }
    
    public static void  SendObjectOnLogQueue(IConfiguration conf, object request)
    {
        var notifyConfig = conf.GetSection("ServiceBusSettings").GetSection("LogQueue");
        string queueName = notifyConfig.GetSection("QueueName").Value ?? throw new Exception("log queue not found");
        ConnectionFactory factory = GetConnectionFactory(notifyConfig);
        SendObjectOnQueue(request,queueName,factory);
    }

    public static void ListeningObjectsInNotificationQueue<T>(Action<T> functionToRun, CancellationToken cancelToken,
        IConfiguration conf)
    {
        var notifyConfig = conf.GetSection("ServiceBusSettings").GetSection("notifyQueue");
        string queueName = notifyConfig.GetSection("QueueName").Value ?? throw new Exception("notify queue not found");
        ListeningObjectsInQueue<T>(functionToRun, cancelToken, queueName,notifyConfig);
    }
    
    public static void ListeningObjectsInLogQueue<T>(Action<T> functionToRun, CancellationToken cancelToken,
        IConfiguration conf)
    {
        var logConfig = conf.GetSection("ServiceBusSettings").GetSection("LogQueue");
        string queueName = logConfig.GetSection("QueueName").Value ?? throw new Exception("Log queue not found");
        ListeningObjectsInQueue<T>(functionToRun, cancelToken, queueName,logConfig);
    }
    
    private static ConnectionFactory GetConnectionFactory(IConfiguration conf)
    {
        
        
        string username = conf.GetSection("Username").Value ?? throw new Exception("username for queue connection not found");  
        string password = conf.GetSection("Password").Value ?? throw new Exception("password for queue connection not found");
        int port = int.Parse(conf.GetSection("Port").Value);
        string hostName = conf.GetSection("HostName").Value ?? throw new Exception("host name for queue connection not found");
        
        
        var factory = new ConnectionFactory
        {
            UserName = username,
            Password = password,
            Port = port,
            HostName = hostName,
        };
        return factory;
    }
    
    public static void  SendObjectOnQueue(object request, string queueName, ConnectionFactory factory)
    {
        
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
    public static void ListeningObjectsInQueue<T>(Action<T> functionToRun, CancellationToken cancelToken, string queueName, IConfiguration conf)
    {
        
        ConnectionFactory factory = GetConnectionFactory(conf);
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