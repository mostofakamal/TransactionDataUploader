using Microsoft.Extensions.Logging;
using TransactionDataUploader.Core.Domain.Enums;
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



        public void ParseFileContentAndSaveData(string content,FileType fileType)
        {
            var parser = FileParserProvider.GetFileParser(fileType);
            var result= parser.ExtractDataFromContent(content);

        }
    }
}
