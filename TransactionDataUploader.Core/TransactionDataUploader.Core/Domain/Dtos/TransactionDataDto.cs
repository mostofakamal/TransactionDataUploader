namespace TransactionDataUploader.Core.Domain.Dtos
{
    public class TransactionDataDto
    {
        public string Id { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }

        public string Date { get; set; }

        public string Status { get; set; }
    }
}