using Microsoft.EntityFrameworkCore;
using POSBackend.Data;
using POSBackend.Helpers;
using POSBackend.Models;
using POSBackend.Models.DTOs;
using POSBackend.Services.Interface;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace POSBackend.Services.Impl
{
    public class ItemService : IItemService
    {
        private readonly AppDbContext _context;
        private readonly CloudinaryHelper _cloudinaryHelper;

        public ItemService(AppDbContext context, CloudinaryHelper cloudinaryHelper)
        {
            _context = context;
            _cloudinaryHelper = cloudinaryHelper;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task<Item> AddItemAsync(ItemDTO itemDto)
        {
            if (string.IsNullOrWhiteSpace(itemDto.Name))
                throw new ArgumentException("Item name is required.");

            if (itemDto.Price <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            if (itemDto.Stock < 0)
                throw new ArgumentException("Stock cannot be negative.");

            if (itemDto.Image == null || itemDto.Image.Length == 0)
                throw new ArgumentException("Image is required.");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(itemDto.Image.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
                throw new ArgumentException("Only image files (.jpg, .jpeg, .png) are allowed.");

            // Upload image to Cloudinary
            string imageUrl = await _cloudinaryHelper.UploadImageAsync(itemDto.Image);

            var item = new Item
            {
                Name = itemDto.Name,
                Price = itemDto.Price,
                Stock = itemDto.Stock,
                Category = itemDto.Category,
                ImageUrl = imageUrl 
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task UpdateItemAsync(int id, ItemDTO itemDto)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
                throw new KeyNotFoundException("Item not found.");

            item.Name = itemDto.Name;
            item.Price = itemDto.Price;
            item.Stock = itemDto.Stock;
            item.Category = itemDto.Category;

            if (itemDto.Image != null)
            {
                if (itemDto.Image.Length > 5 * 1024 * 1024) // Maksimal 5MB
                    throw new ArgumentException("File size exceeds 5MB.");

                var fileExtension = Path.GetExtension(itemDto.Image.FileName).ToLowerInvariant();
                if (!new[] { ".jpg", ".jpeg", ".png" }.Contains(fileExtension))
                    throw new ArgumentException("Only image files (.jpg, .jpeg, .png) are allowed.");

                item.ImageUrl = await _cloudinaryHelper.UploadImageAsync(itemDto.Image);
            }

            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
                throw new KeyNotFoundException("Item not found.");

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
