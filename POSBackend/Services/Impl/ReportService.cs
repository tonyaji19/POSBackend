using Microsoft.EntityFrameworkCore;
using POSBackend.Data;
using POSBackend.Models.DTOs;
using POSBackend.Services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSBackend.Services.Impl
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<POSReportDTO>> GetPOSReportsAsync()
        {
            return await _context.POSTransactions
                .Include(t => t.Item)
                .Select(t => new POSReportDTO
                {
                    TransactionDate = t.TransactionDate,
                    ItemName = t.Item.Name,
                    Category = t.Item.Category,
                    Quantity = t.Quantity,
                    TotalPrice = t.TotalPrice
                })
                .ToListAsync();
        }

        public async Task<List<StockReportDTO>> GetStockReportsAsync()
        {
            return await _context.Items
                .Select(i => new StockReportDTO
                {
                    ItemName = i.Name,
                    Category = i.Category,
                    Stock = i.Stock,
                    Price = i.Price
                })
                .ToListAsync();
        }
    }
}
