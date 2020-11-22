using TransactionDataUploader.Core.Domain.Enums;

namespace TransactionDataUploader.Web.Models
{
    public class FileDataReadResult
    {
        public string Content { get; set; }

        public FileType FileType { get; set; }
    }

}