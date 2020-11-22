using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using TransactionDataUploader.Core.Domain.Enums;
using TransactionDataUploader.Web.Models;

namespace TransactionDataUploader.Web.Utils
{
    public class FileUtility
    {
        /// <summary>
        /// Reads FormFile and return the content
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static async Task<FileDataReadResult> ReadFormFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return await Task.FromResult((FileDataReadResult)null);
            }

            using var reader = new StreamReader(file.OpenReadStream());
            var content=  await reader.ReadToEndAsync();
            var result = new FileDataReadResult
            {
                Content = content,
                FileType = GetFileType(file)
            };
            return result;
        }


        private static FileType GetFileType(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName)?.Replace(".","").ToLower();
            switch (extension)
            {
                case "xml":
                    return FileType.Xml;
                case "csv":
                    return FileType.Csv;
                default:
                    throw new InvalidEnumArgumentException("Not supported file extension");

            }

        }
    }
}
