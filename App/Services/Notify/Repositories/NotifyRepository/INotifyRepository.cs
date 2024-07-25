using Domain.Interfaces;
using Notify.Models;

namespace Notify.Repositories.NotifyRepository;

public interface INotifyRepository<T>: IRepository<T>  where T : class,IEntity
{
    
}