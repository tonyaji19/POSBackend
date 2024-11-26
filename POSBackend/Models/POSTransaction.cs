using System;

namespace POSBackend.Models
{
    public class POSTransaction
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime TransactionDate { get; set; }

        public Item Item { get; set; } 
    }
}
