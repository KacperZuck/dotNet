using Microservice_app.Catalog.Service.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Microservice_app.Catalog.Service.Controller
{
    [ApiController]
    [Route("items")]    // http:// ... /items
    public class ItemsController : ControllerBase
    {
        private static readonly List<ItemDto> items = new()
        {
            new ItemDto(Guid.NewGuid(), "Name1", "des1", 10, DateTimeOffset.Now ),
            new ItemDto(Guid.NewGuid(), "Name2", "des2", 20, DateTimeOffset.Now ),
            new ItemDto(Guid.NewGuid(), "Name3", "des3", 30, DateTimeOffset.Now ),
            new ItemDto(Guid.NewGuid(), "Name4", "des4", 40, DateTimeOffset.Now )
        };

        [HttpGet]  
        public IEnumerable<ItemDto> Get()
        {
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetById(Guid id)
        {
            var item = items.SingleOrDefault(x => x.id == id);

            if (item == null)
                return NotFound();

            return item;
        }

        [HttpPost]
        public ActionResult<ItemDto> Post(CreatedItemDto createdItemDto)
        {
            var item = new ItemDto(
                Guid.NewGuid(),
                createdItemDto.name,
                createdItemDto.description,
                createdItemDto.price,
                DateTimeOffset.Now
            );

            items.Add(item);

            return CreatedAtAction(nameof(GetById), new { id = item.id }, item);
        }

        [HttpPut("{id}")]
        public ActionResult<ItemDto> Put(Guid id, UpdateItemDto updateItemDto)
        {
            var item = items.Where(x => x.id == id).SingleOrDefault();

            if(item == null){ return NotFound();}

            var newItem = item with
            {
                name = updateItemDto.name,
                description = updateItemDto.description,
                price = updateItemDto.price
            };

            var index = items.FindIndex(item => item.id == id );
            items[index] = newItem;

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<ItemDto> Delete(Guid id)
        {
            var index = items.FindIndex(item => item.id == id);
            items.RemoveAt(index);
        
            return Ok();
        }


    }
}