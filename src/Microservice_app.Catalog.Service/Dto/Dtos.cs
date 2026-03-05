using Microsoft.AspNetCore.Mvc;

namespace Microservice_app.Catalog.Service.Dto

{
    public record ItemDto(Guid id, string name, string description, decimal price, DateTimeOffset createdDate)
    {
        public static explicit operator ItemDto(CreatedAtActionResult v)
        {
            throw new NotImplementedException();
        }
    }

    public record CreatedItemDto(string name, string description, decimal price);

    public record UpdateItemDto(string name, string description, decimal price);
}