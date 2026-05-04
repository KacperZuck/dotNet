using System.Linq.Expressions;
using Microservice_app.Common;

namespace Microservice_app.Common
{
    public interface IRepo<T> where T : IEntity // T reprezentuje poszczególną klase jaka ma reprezentowac
    {
        Task CreateAsync(T newItem);
        Task DeleteAsync(T deleteItem);
        Task<IReadOnlyCollection<T>> GetAllAsync( Expression<Func<T, bool>> fillter);
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> fillter);
        Task UpdateAsync(T updatedItem);
    }
}