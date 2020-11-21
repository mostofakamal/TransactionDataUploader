using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionDataUploader.Core.Domain.Dtos;
using TransactionDataUploader.Core.Domain.Enums;
using TransactionDataUploader.Core.Domain.Models;

namespace TransactionDataUploader.Core.Domain.Services
{
    public interface ITransactionDataHandler
    {
        Task<IList<string>> ParseFileContentAndSaveData(string content,FileType fileType);
        Task<List<TransactionDisplayResultDto>> GetTransactions(string currency,DateTime?  fromDate,DateTime? toDate,TransactionStatusId? status);
    }
}