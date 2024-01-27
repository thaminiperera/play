using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Play.Common.Settings;
using MongoDB.Bson;
using Microsoft.Extensions.DependencyInjection;



namespace Play.Common.MongoDB
{
    public static class Extensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services){
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String)); //This is to make sure that the GUIDs are stored as strings in the database
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            

            //Register the MongoDB client as a singleton
            services.AddSingleton(serviceProvider => {

                var configuration = serviceProvider.GetService<IConfiguration>();
                var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var mongoDbSettings = configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();
                var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
                return mongoClient.GetDatabase(serviceSettings.ServiceName);
            });

            return services;
        }

        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName) where T : IEntity
        {
             //Register the repository as a singleton
            services.AddSingleton<IRepository<T>>(ServiceProvider => {
                var database = ServiceProvider.GetService<IMongoDatabase>();
                return new MongoRepository<T>(database, collectionName);

            }); 

            return services;
        }
    }
}