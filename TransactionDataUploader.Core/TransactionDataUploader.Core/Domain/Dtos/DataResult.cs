using System.Collections.Generic;
using System.Linq;

namespace TransactionDataUploader.Core.Domain.Dtos
{
    public class DataResult<T> where T : class
    {
        public IList<string> Errors { get; set; }

        public bool HasError => Errors.Any();

        public IList<T> Data { get; set; }
    }
}
