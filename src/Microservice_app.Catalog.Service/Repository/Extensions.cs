using Microservice_app.Catalog.Service.Model;
using Microservice_app.Catalog.Service.Settings;
using MongoDB.Driver;

namespace Microservice_app.Catalog.Service.Repository
{
    public static class Extensions
    {
        // tworzymy baze danych dla microserwisu obiektowe
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddSingleton(serviceProvider =>
            {
                var Configuration = serviceProvider.GetService<IConfiguration>();
                var serviceSettings = Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var mongoDBSettings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                var mongoClient = new MongoClient(mongoDBSettings.ConnectionString);
                return mongoClient.GetDatabase(serviceSettings.ServiceName);
            });     

            return services;
        }
        // dodajemy stworzoną baze do serwisu
        public static IServiceCollection AddMongoRepo<T>(this IServiceCollection services, string collectionName) 
            where T : IEntity
        {
            services.AddSingleton<IRepo<T>>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();
                return new MongoRepo<T>(database, collectionName);
            });
            return services;
        }
    }
}