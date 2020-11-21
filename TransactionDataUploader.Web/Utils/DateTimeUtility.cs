using System;
using System.Globalization;

namespace TransactionDataUploader.Web.Utils
{
    public class DateTimeUtility
    {
        public static DateTime? ParseDateFromParam(string dateString)
        {
            var result = DateTime.TryParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var date);

            return result ? (DateTime?)date : null;
        }
    }
}
