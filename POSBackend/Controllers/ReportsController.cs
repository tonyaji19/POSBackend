using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POSBackend.Services.Interface;
using System.Threading.Tasks;

namespace POSBackend.Controllers
{
    [Route("api/reports")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // GET: api/reports/pos
        [HttpGet("pos")]
        public async Task<IActionResult> GetPOSReports()
        {
            var reports = await _reportService.GetPOSReportsAsync();
            return Ok(reports);
        }

        // GET: api/reports/stock
        [HttpGet("stock")]
        public async Task<IActionResult> GetStockReports()
        {
            var reports = await _reportService.GetStockReportsAsync();
            return Ok(reports);
        }
    }
}
