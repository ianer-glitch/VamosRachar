using Grpc.Net.Client;

namespace ProtoServer.ConnectionHelpers;

public static  class MicroserviceConnection
{
    public static T GetIdentityClient<T>()
    {
        return GetClient<T>("http://identity:5001");
    }
    
    public static T GetNotifyClient<T>()
    {
        return GetClient<T>("http://notify:5002");
    }
    
    private static T GetClient<T>(string clientUrl)
    {
        try
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            var channel = GrpcChannel.ForAddress(clientUrl, new GrpcChannelOptions { HttpHandler = handler });
            
            var client = (T)Activator.CreateInstance(typeof(T),channel);
            
            if (client is null)
                throw new ArgumentNullException("Client Grpc Not Found");
            
            return client;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}