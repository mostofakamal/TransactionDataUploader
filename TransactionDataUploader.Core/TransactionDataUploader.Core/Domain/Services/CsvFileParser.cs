using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using TinyCsvParser;
using TransactionDataUploader.Core.Domain.Dtos;
using TransactionDataUploader.Core.Domain.Dtos.Mappings;
using TransactionDataUploader.Core.Domain.Models;
using TransactionDataUploader.Core.Domain.Validators;
using TransactionDataUploader.Core.Utilities.Constants;

namespace TransactionDataUploader.Core.Domain.Services
{
    public class CsvFileParser : FileParser<TransactionDataDto, TransactionEntry>
    {
        protected override List<TransactionDataDto> ParseData(string content)
        {
            var csvParserOptions = new CsvParserOptions(false, ',');
            var csvParser = new CsvParser<TransactionDataDto>(csvParserOptions, new TransactionDataDtoCsvMapping());
            var records = csvParser.ReadFromString(new CsvReaderOptions(new[] { Environment.NewLine }), content).ToList();
            return records.Select(x => x.Result).ToList();
        }

        protected override AbstractValidator<TransactionDataDto> Validator { get; } = new TransactionDataValidatorForCsv();

        protected override List<TransactionEntry> Map(List<TransactionDataDto> data)
        {
            return data.Select(x => new TransactionEntry
            {
                CurrencyCode = x.Currency,
                Amount = Convert.ToDecimal(x.Amount),
                TransactionStatusId = _statusMapping[x.Status],
                TransactionDate = DateTime.ParseExact(x.Date, DateTimeFormats.CsvDateTimeFormat, null),
                TransactionId = x.Id

            }).ToList();
        }
        readonly Dictionary<string, TransactionStatusId> _statusMapping = new Dictionary<string, TransactionStatusId>()
        {
            { "Approved",TransactionStatusId.A},
            { "Failed",TransactionStatusId.R},
            { "Finished",TransactionStatusId.D},
        };
    }
}