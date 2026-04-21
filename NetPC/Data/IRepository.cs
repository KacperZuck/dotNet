using NetPC.Models;
using System.Linq.Expressions;
using System.Security.Principal;

namespace NetPC.Data
{
    public interface IRepository<T> where T : class, IEntity
    {

        Task Create(T entity);
        Task Delete(Guid id);
        Task<IReadOnlyCollection<T>> GetAll();
        Task <T> GetbyId(Guid id);
        Task Update(T entity);
    }
}
