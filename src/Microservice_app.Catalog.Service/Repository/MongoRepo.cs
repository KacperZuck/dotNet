using Microservice_app.Catalog.Service.Model;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Bindings;

namespace Microservice_app.Catalog.Service.Repository
{

    public class MongoRepo<T> : IRepo<T> where T : IEntity
    {
        private readonly IMongoCollection<T> dbCollection;

        private readonly FilterDefinitionBuilder<T> fillterBuilder = Builders<T>.Filter;

        public MongoRepo(IMongoDatabase database, string collectionName)   // definiujemy w jaki dokładnie sposób bedziemy sie łączyc z baza 
        {
            dbCollection = database.GetCollection<T>(collectionName);
        }

        // IReadOnlyCollection -- zapewnia ze klient bedzie tylko odczytywał dane ( optymalizacja )
        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await dbCollection.Find(fillterBuilder.Empty).ToListAsync();
        }

        public async Task<T> GetItemAsync(Guid id)
        {
            FilterDefinition<T> filter = fillterBuilder.Where(item => item.id == id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateItemAsync(T newItem)
        {
            if (newItem == null)
            {
                throw new ArgumentNullException(nameof(newItem));
            }

            await dbCollection.InsertOneAsync(newItem); // asynchronicznie 
        }

        public async Task UpdateItemAsync(T updatedItem)
        {
            if (updatedItem == null)
            {
                throw new ArgumentNullException(nameof(updatedItem));
            }
            await dbCollection.FindOneAndReplaceAsync(fillterBuilder.Where(item => item.id == updatedItem.id), updatedItem);
        }

        public async Task DeleteItemAsync(T deleteItem)
        {
            if (deleteItem == null)
            {
                throw new ArgumentNullException(nameof(deleteItem));
            }
            await dbCollection.FindOneAndDeleteAsync(fillterBuilder.Where(item => item.id == deleteItem.id));
        }
    }
}