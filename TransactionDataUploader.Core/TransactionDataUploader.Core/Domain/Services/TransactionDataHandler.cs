using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TransactionDataUploader.Core.Domain.Dtos;
using TransactionDataUploader.Core.Domain.Enums;
using TransactionDataUploader.Core.Domain.Models;
using TransactionDataUploader.Core.Infrastructure.Persistence;

namespace TransactionDataUploader.Core.Domain.Services
{
    public class TransactionDataHandler : ITransactionDataHandler
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<TransactionDataHandler> _logger;

        public TransactionDataHandler(ITransactionRepository transactionRepository, ILogger<TransactionDataHandler> logger)
        {
            _transactionRepository = transactionRepository;
            _logger = logger;
        }

        public async Task<IList<string>> ParseFileContentAndSaveData(string content,FileType fileType)
        {
            var parser = FileParserProvider.GetFileParser(fileType);
            var result= parser.ExtractDataFromContent(content);
            if (!result.HasError && result.Data.Any())
            {
                _logger.LogInformation($"Transaction data of file type {fileType} saved to database successfully! ");
                await _transactionRepository.AddRangeAsync(result.Data.ToList());
            }
            else
            {
                _logger.LogWarning($"Validation error during file upload of type: {fileType} .  Errors: {string.Join(",", result.Errors)}  , FileContent: {content} ");
            }

            return result.Errors;
        }

        public async Task<List<TransactionDisplayResultDto>> GetTransactions(string currency,DateTime?  fromDate,DateTime? toDate,TransactionStatusId? status)
        {
            var transactions= await _transactionRepository.GetAllTransactions(currency, fromDate, toDate, status);
            var transactionDisplayResult = transactions.Select(x => new TransactionDisplayResultDto()
            {
                Id = x.TransactionId,
                Payment = $"{x.Amount} {x.CurrencyCode}",
                Status = x.TransactionStatus.Name
            }).ToList();
            return transactionDisplayResult;
        }
    }
}
