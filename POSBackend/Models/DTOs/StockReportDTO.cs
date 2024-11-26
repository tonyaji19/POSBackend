namespace POSBackend.Models.DTOs
{
    public class StockReportDTO
    {
        public string ItemName { get; set; }
        public string Category { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
