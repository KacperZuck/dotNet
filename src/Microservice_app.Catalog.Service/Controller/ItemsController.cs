using System.ComponentModel;
using Microservice_app.Catalog.Service.Dto;
using Microservice_app.Catalog.Service.Model;
using Microservice_app.Catalog.Service.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Microservice_app.Catalog.Service.Controller
{
    [ApiController]
    [Route("items")]    // http:// ... /items
    public class ItemsController : ControllerBase
    {
        private readonly IRepo<Item> itemRepo; // metody sa ASYNCHRONICZNE !!!! wiec tutaj tez trzeba zamienic

        public ItemsController(IRepo<Item> itemRepo)
        {
            this.itemRepo = itemRepo;
        }
        [HttpGet]  
        public async Task<IEnumerable<ItemDto>> Get()
        {
            var items = (await itemRepo.GetAllAsync()).Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetById(Guid id)
        {
            var item = (await itemRepo.GetItemAsync(id)).AsDto();

            if (item == null)
                return NotFound();

            return item;
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> Post(CreatedItemDto createdItemDto)
        {
            var item = new Item
            {
                id = Guid.NewGuid(),
                name = createdItemDto.name,
                description = createdItemDto.description,
                price = createdItemDto.price,
                createdDate = DateTimeOffset.UtcNow
            };

            await itemRepo.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetById), new { id = item.id }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ItemDto>> Put(Guid id, UpdateItemDto updateItemDto)
        {
            var item = await itemRepo.GetItemAsync(id);
            if(item == null){ return NotFound();}

            item.name = updateItemDto.name;
            item.price = updateItemDto.price;
            item.description = updateItemDto.description;
            await itemRepo.UpdateItemAsync(item);
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemDto>> Delete(Guid id)
        {
            var item = await itemRepo.GetItemAsync(id);
            if (item == null){ return NotFound();}
            
            await itemRepo.DeleteItemAsync(item);
        
            return Ok();
        }


    }
}