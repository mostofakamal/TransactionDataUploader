using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using FluentValidation;
using TransactionDataUploader.Core.Domain.Dtos;
using TransactionDataUploader.Core.Domain.Models;
using TransactionDataUploader.Core.Domain.Validators;
using TransactionDataUploader.Core.Utilities.Constants;

namespace TransactionDataUploader.Core.Domain.Services
{
    public class XmlFileParser : FileParser<TransactionDataDto, TransactionEntry>
    {
        protected override List<TransactionDataDto> ParseData(string content)
        {
            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(content);
                MemoryStream stream = new MemoryStream(byteArray);

                var s = new System.Xml.Serialization.XmlSerializer(typeof(Transactions));
                var xmlReader = XmlReader.Create(stream);
                var transactions = (Transactions) s.Deserialize(xmlReader);
                return transactions.Transaction.Select(x => new TransactionDataDto()
                {
                    Amount = x.PaymentDetails.Amount.ToString(CultureInfo.InvariantCulture),
                    Currency = x.PaymentDetails.CurrencyCode,
                    Date = x.TransactionDate,
                    Id = x.id,
                    Status = x.Status
                }).ToList();
            }
            catch (Exception)
            {
                //TODO: Log error
                return null;
            }

          
        }

        protected override AbstractValidator<TransactionDataDto> Validator { get; } = new TransactionDataValidatorForXml();


        protected override List<TransactionEntry> Map(List<TransactionDataDto> data)
        {
            return data.Select(x => new TransactionEntry
            {
                CurrencyCode = x.Currency,
                TransactionStatusId = _statusMapping[x.Status],
                TransactionDate = DateTime.ParseExact(x.Date, DateTimeFormats.XmlDateTimeFormat, CultureInfo.InvariantCulture),
                Amount = Convert.ToDecimal(x.Amount),
                TransactionId = x.Id

            }).ToList();
        }

        readonly Dictionary<string, TransactionStatusId> _statusMapping = new Dictionary<string, TransactionStatusId>
        {
            { "Approved",TransactionStatusId.A},
            { "Rejected",TransactionStatusId.R},
            { "Done",TransactionStatusId.D},
        };
    }
}