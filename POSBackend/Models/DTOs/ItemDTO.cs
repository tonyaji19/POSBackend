namespace POSBackend.Models.DTOs
{
    public class ItemDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }
        public IFormFile Image { get; set; }
    }
}
