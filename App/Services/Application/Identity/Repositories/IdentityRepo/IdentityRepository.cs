namespace Identity.Repositories.IdentityRepo;

public class IdentityRepository<T> : IIdentityRepository<T>
{
    public Task<T1> GetAllPaging<T1>(int Quanity, int CurrenctPage)
    {
        throw new NotImplementedException();
    }

    public Task<T> InsertOrUpdate(T Entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> SetExcluded(T Entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> SetActive(T Entity)
    {
        throw new NotImplementedException();
    }
}