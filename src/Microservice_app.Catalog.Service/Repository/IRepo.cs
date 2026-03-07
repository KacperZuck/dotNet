using Microservice_app.Catalog.Service.Model;

namespace Microservice_app.Catalog.Service.Repository
{
    public interface IRepo<T> where T : IEntity // T reprezentuje poszczególną klase jaka ma reprezentowac
    {
        Task CreateItemAsync(T newItem);
        Task DeleteItemAsync(T deleteItem);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<T> GetItemAsync(Guid id);
        Task UpdateItemAsync(T updatedItem);
    }
}