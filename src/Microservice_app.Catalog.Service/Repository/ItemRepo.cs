using Microservice_app.Catalog.Service.Model;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Bindings;

namespace Microservice_app.Catalog.Service.Repository
{
    public class ItemRepo
    {
        private const string collectionName = "item";   // nazwa kolekcji w bazie

        private readonly IMongoCollection<Item> dbCollection;

        private readonly FilterDefinitionBuilder<Item> fillterBuilder = Builders<Item>.Filter;

        public ItemRepo()   // definiujemy w jaki dokładnie sposób bedziemy sie łączyc z baza 
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017"); 
            var datebase = mongoClient.GetDatabase("Catalog"); // obiekt który bedzie reprezentował rzeczywista baze 
            dbCollection = datebase.GetCollection<Item>(collectionName);        
        }

        // IReadOnlyCollection -- zapewnia ze klient bedzie tylko odczytywał dane ( optymalizacja )
        public async Task<IReadOnlyCollection<Item>> GetAllAsync()
        {
            return await dbCollection.Find(fillterBuilder.Empty).ToListAsync();
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            FilterDefinition<Item> filter = fillterBuilder.Where(item => item.id == id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateItemAsync(Item newItem)
        {
            if(newItem == null)
            {
                throw new ArgumentNullException(nameof(newItem));
            }

            await dbCollection.InsertOneAsync(newItem); // asynchronicznie 
        }

        public async Task UpdateItemAsync(Item updatedItem)
        {
            if(updatedItem == null)
            {
                throw new ArgumentNullException(nameof(updatedItem));
            }
            await dbCollection.FindOneAndReplaceAsync( fillterBuilder.Where(item => item.id == updatedItem.id), updatedItem);
        }

        public async Task DeleteItemAsync(Item deleteItem)
        {
            if(deleteItem == null)
            {
                throw new ArgumentNullException(nameof(deleteItem));
            }
            await dbCollection.FindOneAndDeleteAsync(fillterBuilder.Where(item => item.id == deleteItem.id));
        }
    }
}