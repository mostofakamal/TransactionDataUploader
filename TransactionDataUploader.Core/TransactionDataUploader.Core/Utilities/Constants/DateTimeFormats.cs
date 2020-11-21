namespace TransactionDataUploader.Core.Utilities.Constants
{
    public static class DateTimeFormats
    {
        public const string CsvDateTimeFormat = "dd/MM/yyyy hh:mm:ss";
        public const string XmlDateTimeFormat = "yyyy-MM-ddTHH:mm:ss";
    }

    public static class ErrorMessages
    {
        public const string TransactionIdLengthValidationError =
            "Transaction Id Can not be greater than 50 in length. ";

        public const string AmountNotValidError = "Amount must be a valid decimal number. ";

        public const string InvalidCurrencyCodeError = "Currency Code is not a Valid ISO4217 format code. ";

        public const string InvalidDateTime = "Invalid Datetime. ";

    }

}
