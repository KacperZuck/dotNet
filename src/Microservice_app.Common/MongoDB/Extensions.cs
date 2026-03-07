using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Microservice_app.Common.Settings;

namespace Microservice_app.Common.MongoDB
{
    public static class Extensions
    {
        // tworzymy baze danych dla microserwisu obiektowe
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddSingleton(serviceProvider =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var mongoDBSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
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