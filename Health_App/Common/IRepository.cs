using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Health_App.Common
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T?> Get(Guid id);
        Task<ICollection<T>> GetAll();
        Task Update(T entity);
        Task Add(T entity);
        Task Delete(Guid id);
    }
}
