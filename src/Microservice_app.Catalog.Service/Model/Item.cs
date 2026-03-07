using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Microservice_app.Catalog.Service.Model
{

    public class Item : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid id { get; set; }
        public string name { set; get; }
        public string description { set; get; }
        public decimal price { set; get; }
        public DateTimeOffset createdDate { set; get; }
    }
}