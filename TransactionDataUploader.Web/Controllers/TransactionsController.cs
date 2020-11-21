using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransactionDataUploader.Core.Domain.Models;
using TransactionDataUploader.Core.Domain.Services;

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

        public async Task<IActionResult> Get([FromQuery]string currency,[FromQuery]string fromDate,[FromQuery]string toDate,[FromQuery]string status)
        {

            var isValidStatus = Enum.TryParse(status,out TransactionStatusId statusId);
            var transactions = await _transactionDataHandler.GetTransactions(currency, null,null, isValidStatus?  (TransactionStatusId?)statusId: null);
            return Ok(transactions);
        }
    }
}
