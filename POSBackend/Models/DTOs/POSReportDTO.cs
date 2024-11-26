using System;

namespace POSBackend.Models.DTOs
{
    public class POSReportDTO
    {
        public DateTime TransactionDate { get; set; }
        public string ItemName { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
