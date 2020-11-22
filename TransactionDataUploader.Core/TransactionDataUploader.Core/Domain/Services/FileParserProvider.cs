using System.ComponentModel;
using TransactionDataUploader.Core.Domain.Dtos;
using TransactionDataUploader.Core.Domain.Enums;
using TransactionDataUploader.Core.Domain.Models;

namespace TransactionDataUploader.Core.Domain.Services
{
    public  class FileParserProvider
    {
        public static FileParser<TransactionDataDto, TransactionEntry> GetFileParser(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.Csv:
                    return new CsvFileParser();
                case FileType.Xml:
                    return new XmlFileParser();
                default:
                    throw new InvalidEnumArgumentException("Invalid file type");
            }
        }
    }
}
