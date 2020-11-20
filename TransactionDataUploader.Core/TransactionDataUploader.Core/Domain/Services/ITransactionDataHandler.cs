using TransactionDataUploader.Core.Domain.Enums;

namespace TransactionDataUploader.Core.Domain.Services
{
    public interface ITransactionDataHandler
    {
        void ParseFileContentAndSaveData(string content,FileType fileType);
    }
}