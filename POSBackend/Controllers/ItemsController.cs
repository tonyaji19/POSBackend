using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POSBackend.Models;
using POSBackend.Models.DTOs;
using POSBackend.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POSBackend.Controllers
{
    [Route("api/items")]
    [ApiController]
    [Authorize] // Pastikan hanya yang terautentikasi yang bisa mengakses endpoint ini
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return Ok(await _itemService.GetItemsAsync());
        }

        // GET: api/items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // POST: api/items
        [HttpPost]
        public async Task<ActionResult> AddItem([FromForm] ItemDTO itemDto)
        {
            var createdItem = await _itemService.AddItemAsync(itemDto);

            return CreatedAtAction(nameof(GetItem), new { id = createdItem.Id }, new
            {
                message = "Successfully added item.",
                item = createdItem
            });
        }

        // PUT: api/items/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(int id, [FromForm] ItemDTO itemDto)
        {
            try
            {
                await _itemService.UpdateItemAsync(id, itemDto);
                return Ok(new { message = "Item successfully updated." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Item not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the item.", details = ex.Message });
            }
        }

        // DELETE: api/items/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            try
            {
                await _itemService.DeleteItemAsync(id);
                return Ok(new { message = "Item successfully deleted." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Item not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the item.", details = ex.Message });
            }
        }
    }
}
