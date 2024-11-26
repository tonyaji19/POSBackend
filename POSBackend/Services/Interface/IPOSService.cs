using POSBackend.Models;
using POSBackend.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POSBackend.Services.Interface
{
    public interface IPOSService
    {
        Task<List<POSTransaction>> GetTransactionsAsync();
        Task<POSTransaction> AddTransactionAsync(POSTransactionDTO transactionDto);
    }
}
