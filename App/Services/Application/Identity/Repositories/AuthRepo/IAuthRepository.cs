using ProtoServer.ProtoFiles;

namespace Identity.Repositories.AuthRepo;

public interface IAuthRepository
{
    Task<bool> IsUserPasswordValid(PLoginRequest request);
}