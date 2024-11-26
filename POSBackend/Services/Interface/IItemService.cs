using POSBackend.Models;
using POSBackend.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POSBackend.Services.Interface
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetItemsAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task<Item> AddItemAsync(ItemDTO itemDto);
        Task UpdateItemAsync(int id, ItemDTO itemDto);
        Task DeleteItemAsync(int id);
    }
}
