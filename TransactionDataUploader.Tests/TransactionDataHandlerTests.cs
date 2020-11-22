using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using TransactionDataUploader.Core.Domain.Enums;
using TransactionDataUploader.Core.Domain.Models;
using TransactionDataUploader.Core.Domain.Services;
using TransactionDataUploader.Core.Infrastructure.Persistence;
using Xunit;

namespace TransactionDataUploader.Tests
{
    public class TransactionDataHandlerTests
    {
        private readonly ITransactionDataHandler _transactionDataHandler;
        private readonly Mock<ITransactionRepository> _mockRepo;
        public TransactionDataHandlerTests()
        {
            _mockRepo = new Mock<ITransactionRepository>();
            var mockLogger = new Mock<ILogger<TransactionDataHandler>>();
            _transactionDataHandler = new TransactionDataHandler(_mockRepo.Object, mockLogger.Object);
        }

        [Fact]
        public async Task TestParseFileContentAndSaveData_FileContentValidationError_DataNotSaved()
        {
            var fileContent = "\"Invoice0000001\",\"InvalidAmount\", \"USD\", \"20/02/2019 12:33:16\", \"Approved\"";
            await _transactionDataHandler.ParseFileContentAndSaveData(fileContent, FileType.Csv);
            _mockRepo.Verify(x=>x.AddRangeAsync(It.IsAny<List<TransactionEntry>>()),Times.Never);
        }

        [Fact]
        public async Task TestParseFileContentAndSaveData_FileContentOk_DataSaved()
        {
            var fileContent = "\"Invoice0000001\",\"200\", \"USD\", \"20/02/2019 12:33:16\", \"Approved\"";
            await _transactionDataHandler.ParseFileContentAndSaveData(fileContent, FileType.Csv);
            _mockRepo.Verify(x => x.AddRangeAsync(It.IsAny<List<TransactionEntry>>()), Times.Once);
        }
    }
}