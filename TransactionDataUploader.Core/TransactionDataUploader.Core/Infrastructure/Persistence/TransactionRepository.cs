using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransactionDataUploader.Core.Domain.Models;

namespace TransactionDataUploader.Core.Infrastructure.Persistence
{
    public class TransactionRepository : Repository<TransactionEntry>, ITransactionRepository
    {
        public TransactionRepository(TransactionContext transactionContext) : base(transactionContext)
        {
        }


        public Task<TransactionEntry> GetTransactionById(string id)
        {
            return GetAll().FirstOrDefaultAsync(x => x.TransactionId == id);
        }

        public Task<List<TransactionEntry>> GetAllTransactions()
        {
            return GetAll().Include(x => x.TransactionStatus).ToListAsync();
        }


        public Task<List<TransactionEntry>> GetAllTransactions(string currency, DateTime? fromDate, DateTime? toDate, TransactionStatusId? status)
        {
            var transactions = GetAll();
            if (!string.IsNullOrEmpty(currency))
            {
                transactions = transactions.Where(x => x.CurrencyCode == currency);
            }

            if (fromDate != null && toDate != null)
            {
                transactions = transactions.Where(x => x.TransactionDate >= fromDate && x.TransactionDate <= toDate);
            }

            if (status.HasValue)
            {
                transactions = transactions.Where(x => x.TransactionStatusId == status.Value);
            }

            return transactions.ToListAsync();
        }

    }
}