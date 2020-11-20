using System;

namespace TransactionDataUploader.Core.Domain.Models
{
    public class TransactionEntry
    {
        public long Id { get; set; }

        public string TransactionId { get; set; }

        public decimal Amount { get; set; }
        
        public string CurrencyCode { get; set; }

        public DateTime TransactionDate { get; set; }

        public TransactionStatus TransactionStatus { get; set; }

        public TransactionStatusId TransactionStatusId { get; set; }
    }
}
