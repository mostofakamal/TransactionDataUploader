using TinyCsvParser.Mapping;

namespace TransactionDataUploader.Core.Domain.Dtos.Mappings
{
    class TransactionDataDtoCsvMapping : CsvMapping<TransactionDataDto>
    {
        public TransactionDataDtoCsvMapping() : base()
        {
            MapProperty(0, x => x.Id);
            MapProperty(1, x => x.Amount);
            MapProperty(2, x => x.Currency);
            MapProperty(3, x => x.Date);
            MapProperty(4, x => x.Status);
        }
    }

}
