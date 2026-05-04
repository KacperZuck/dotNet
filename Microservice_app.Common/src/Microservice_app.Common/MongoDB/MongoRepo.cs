using System.Linq.Expressions;
using MongoDB.Driver;

namespace Microservice_app.Common.MongoDB
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

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> fillter)
        {
            return await dbCollection.Find(fillter).ToListAsync();
        }
        
        public async Task<T> GetAsync(Guid id)
        {
            FilterDefinition<T> filter = fillterBuilder.Where(item => item.id == id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> fillter)
        {
            return await dbCollection.Find(fillter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T newItem)
        {
            if (newItem == null)
            {
                throw new ArgumentNullException(nameof(newItem));
            }

            await dbCollection.InsertOneAsync(newItem); // asynchronicznie 
        }

        public async Task UpdateAsync(T updatedItem)
        {
            if (updatedItem == null)
            {
                throw new ArgumentNullException(nameof(updatedItem));
            }
            await dbCollection.FindOneAndReplaceAsync(fillterBuilder.Where(item => item.id == updatedItem.id), updatedItem);
        }

        public async Task DeleteAsync(T deleteItem)
        {
            if (deleteItem == null)
            {
                throw new ArgumentNullException(nameof(deleteItem));
            }
            await dbCollection.FindOneAndDeleteAsync(fillterBuilder.Where(item => item.id == deleteItem.id));
        }
    }
}