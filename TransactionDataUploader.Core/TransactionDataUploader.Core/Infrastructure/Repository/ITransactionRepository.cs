using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionDataUploader.Core.Domain.Models;

namespace TransactionDataUploader.Core.Infrastructure.Repository
{
    public interface ITransactionRepository : IRepository<TransactionEntry>
    {
        Task<TransactionEntry> GetTransactionById(string id);
        Task<List<TransactionEntry>> GetAllTransactions();
        Task<List<TransactionEntry>> GetAllTransactions(string currency, DateTime? fromDate, DateTime? toDate, TransactionStatusId? status);
    }
}
