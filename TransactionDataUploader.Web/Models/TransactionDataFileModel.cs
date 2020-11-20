using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TransactionDataUploader.Web.Attributes;

namespace TransactionDataUploader.Web.Models
{
    public class TransactionDataFileModel
    {
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(1048576)]
        [AllowedExtensions(new string[] { ".xml", ".csv" })]
        public IFormFile File { get; set; }
    }
}