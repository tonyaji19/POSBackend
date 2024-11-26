using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POSBackend.Models.DTOs;
using POSBackend.Services.Interface;
using System.Threading.Tasks;

namespace POSBackend.Controllers
{
    [Route("api/pos")]
    [ApiController]
    [Authorize]
    public class POSController : ControllerBase
    {
        private readonly IPOSService _posService;

        public POSController(IPOSService posService)
        {
            _posService = posService;
        }

        // GET: api/pos/transactions
        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await _posService.GetTransactionsAsync();
            return Ok(transactions);
        }

        // POST: api/pos/transactions
        [HttpPost("transactions")]
        public async Task<IActionResult> AddTransaction([FromBody] POSTransactionDTO transactionDto)
        {
            try
            {
                // Proses transaksi
                var transaction = await _posService.AddTransactionAsync(transactionDto);

                // Kembalikan hasil transaksi
                return Ok(new
                {
                    message = "Transaction successful!",
                    transaction = new
                    {
                        transaction.Id,
                        transaction.ItemId,
                        transaction.Item.Name,
                        transaction.Quantity,
                        transaction.Price,
                        transaction.TotalPrice,
                        transaction.TransactionDate
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
