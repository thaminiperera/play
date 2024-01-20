using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;

namespace Play.Catalog.Service.Controllers
{ 
    [ApiController]
    [Route("items")]
    public class ItemController : ControllerBase
    {
        // Create a list of items using ItemDto
        private static readonly List<ItemDto> items = new()
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison", 7, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Bronze sword", "Deals a small amount of damage", 20, DateTimeOffset.UtcNow)
        };

        //GET all items
        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return items;
        }

        //GET an item using its ID
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetById(Guid id)
        {
            var item = items.Find(item => item.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        //POST an item
        [HttpPost]
        public ActionResult<ItemDto> Post(CreateItemDto createItemDto)
        {
            var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
            items.Add(item); // Add the item to the list 

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item); //It uses the GetById created above to return the item
        }

        //PUT (update) an item using its ID
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = items.Find(item => item.Id == id);
            if(existingItem == null)
            {
                return NotFound();
            }

            var updatedItem = existingItem with
            {
                Name = updateItemDto.Name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price
            };

            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items[index] = updatedItem;

            return NoContent();
        }

        //DELETE an item using its ID
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            if(index < 0)
            {
                return NotFound();
            }

            items.RemoveAt(index);

            return NoContent();
        }
    }
}