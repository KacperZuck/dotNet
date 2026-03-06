using Microservice_app.Catalog.Service.Dto;
using Microservice_app.Catalog.Service.Model;

namespace Microservice_app.Catalog.Service
{
    public static class Extension
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.id, item.name, item.description, item.price, item.createdDate);
        }
    }
}