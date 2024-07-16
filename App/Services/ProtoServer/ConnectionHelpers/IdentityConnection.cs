using Grpc.Net.Client;

namespace ProtoServer.ConnectionHelpers;

public static  class MicroserviceConnection
{
    public static T GetIdentityClient<T>()
    {
        try
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            var channel = GrpcChannel.ForAddress("http://identity:5001", new GrpcChannelOptions { HttpHandler = handler });
            
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