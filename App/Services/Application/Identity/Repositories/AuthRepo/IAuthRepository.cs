using ProtoServer.ProtoFiles;

namespace Identity.Repositories.AuthRepo;

public interface IAuthRepository
{
    Task<PLoginRequest> UserLogin(PLoginRequest request);
}