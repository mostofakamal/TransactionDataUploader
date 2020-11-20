using System;
using System.Linq;
using FluentValidation;
using TransactionDataUploader.Core.Domain.Dtos;
using TransactionDataUploader.Core.Utilities.Constants;
using TransactionDataUploader.Core.Utilities.Helpers;

namespace TransactionDataUploader.Core.Domain.Validators
{
    public class TransactionDataValidatorForCsv : AbstractValidator<TransactionDataDto>
    {
        public TransactionDataValidatorForCsv()
        {
            RuleFor(x => x.Id).NotEmpty().MaximumLength(50).WithMessage(ErrorMessages.TransactionIdLengthValidationError);
            RuleFor(x => x.Amount).NotEmpty().Must((x) => decimal.TryParse(x, out var number)).WithMessage(ErrorMessages.AmountNotValidError);
            RuleFor(x => x.Currency).NotEmpty().Must(x => StaticDataProvider.GetAllIso4217CurrencyCodes().Contains(x))
                .WithMessage(ErrorMessages.InvalidCurrencyCodeError);
            RuleFor(x => x.Date).NotEmpty().Must((x) => DateTime.TryParseExact(x, DateTimeFormats.CsvDateTimeFormat, System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out var number)).WithMessage(ErrorMessages.InvalidDateTime);

            RuleFor(x => x.Status).NotEmpty().Must(x => StaticDataProvider.GetCsvDataTransactionsStatues().Contains(x))
                .WithMessage($"{{PropertyValue}} not a valid Transaction Status for CSV Transaction Data");
        }
    }
}
