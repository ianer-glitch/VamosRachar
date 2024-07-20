using Grpc.Core;
using ProtoServer.ProtoFiles;

namespace Identity.UseCases.AuthUseCase;

public class AuthUseCase : ProtoServer.ProtoFiles.AuthUseCase.AuthUseCaseBase
{
    public override async Task<PLoginRequest> UserLogin(PLoginRequest request, ServerCallContext context)
    {
        try
        {
                
            return await Task.FromResult(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}