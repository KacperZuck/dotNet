using System;
using Microservice_app.Common;

namespace Microservice_app.Catalog.Service.Model
{

    public class Item : IEntity
    {
        public Guid id { get; set; }
        public string name { set; get; }
        public string description { set; get; }
        public decimal price { set; get; }
        public DateTimeOffset createdDate { set; get; }
    }
}