using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransactionDataUploader.Core.Domain.Models;
using TransactionDataUploader.Core.Domain.Services;
using TransactionDataUploader.Web.Utils;

namespace TransactionDataUploader.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {

        private readonly ITransactionDataHandler _transactionDataHandler;

        public TransactionsController(ITransactionDataHandler transactionDataHandler)
        {
            _transactionDataHandler = transactionDataHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string currency, [FromQuery] string fromDate, [FromQuery] string toDate, [FromQuery] string status)
        {
            var startDate = DateTimeUtility.ParseDateFromParam(fromDate);
            var endDate = DateTimeUtility.ParseDateFromParam(toDate);

            var isValidStatus = Enum.TryParse(status, out TransactionStatusId statusId);
            var transactions = await _transactionDataHandler.GetTransactions(currency, startDate, endDate, isValidStatus ? (TransactionStatusId?)statusId : null);
            return Ok(transactions);
        }

    }
}
