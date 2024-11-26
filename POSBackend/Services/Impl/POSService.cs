using Microsoft.EntityFrameworkCore;
using POSBackend.Data;
using POSBackend.Models;
using POSBackend.Models.DTOs;
using POSBackend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSBackend.Services.Impl
{
    public class POSService : IPOSService
    {
        private readonly AppDbContext _context;

        public POSService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<POSTransaction>> GetTransactionsAsync()
        {
            return _context.POSTransactions
                .Include(t => t.Item)
                .OrderByDescending(t => t.TransactionDate)
                .ToList();
        }

        public async Task<POSTransaction> AddTransactionAsync(POSTransactionDTO transactionDto)
        {
            var item = _context.Items.FirstOrDefault(i => i.Id == transactionDto.ItemId);
            if (item == null || item.Stock < transactionDto.Quantity)
            {
                throw new Exception("Item not found or insufficient stock.");
            }

            item.Stock -= transactionDto.Quantity;

            var transaction = new POSTransaction
            {
                ItemId = transactionDto.ItemId,
                Quantity = transactionDto.Quantity,
                Price = item.Price,
                TotalPrice = item.Price * transactionDto.Quantity,
                TransactionDate = DateTime.Now
            };

            _context.POSTransactions.Add(transaction);
            await _context.SaveChangesAsync();

            transaction.Item = item;
            return transaction;
        }

    }
}
