using POSBackend.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POSBackend.Services.Interface
{
    public interface IReportService
    {
        Task<List<POSReportDTO>> GetPOSReportsAsync();
        Task<List<StockReportDTO>> GetStockReportsAsync();
    }
}
