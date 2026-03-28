using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Hosting;

namespace Microservice_app.Catalog.Service.Dto

{
    public record ItemDto(Guid id, string name, string description, decimal price, DateTimeOffset createdDate);

    public record CreatedItemDto([Required] string name, string description,[Required][Range(1,1000)] decimal price);

    public record UpdateItemDto([Required] string name, string description,[Required][Range(0,1000)] decimal price);
}