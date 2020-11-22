using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransactionDataUploader.Core.Domain.Models;
using TransactionDataUploader.Core.Domain.Services;
using TransactionDataUploader.Web.Models;
using TransactionDataUploader.Web.Utils;

namespace TransactionDataUploader.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITransactionDataHandler _transactionDataHandler;

        public HomeController(ILogger<HomeController> logger, ITransactionDataHandler transactionDataHandler)
        {
            _logger = logger;
            _transactionDataHandler = transactionDataHandler;
        }

        public IActionResult Index()
        {
            var model = new TransactionDataFileModel();
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Save(TransactionDataFileModel transactionDataModel)
        {
            if (ModelState.IsValid)
            {
                var errors = await ProcessFile(transactionDataModel);
                if (errors.Any())
                {
                    var model = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = string.Join('\n', errors) };
                    return View("Error", model);
                }

                ViewData["message"] = "File uploaded successfully!";
                return View("Index", new TransactionDataFileModel());
            }
            return View(nameof(Index), transactionDataModel);

        }

        private async Task<IList<string>> ProcessFile(TransactionDataFileModel transactionDataModel)
        {
            var transactionFile = transactionDataModel.File;
            if (transactionFile == null || transactionFile.Length == 0)
            {
                throw new InvalidOperationException("File is not selected or is empty");
            }

            var fileReadResult = await FileUtility.ReadFormFileAsync(transactionFile);
            var errors =
                await _transactionDataHandler.ParseFileContentAndSaveData(fileReadResult.Content,
                    fileReadResult.FileType);
            return errors;
        }

        public async Task<IActionResult> Search(string currency, string status, string fromDate, string toDate)
        {
            var isValidStatus = Enum.TryParse(status, out TransactionStatusId statusId);
            var startDate = DateTimeUtility.ParseDateFromParam(fromDate);
            var endDate = DateTimeUtility.ParseDateFromParam(toDate);
            var result = await _transactionDataHandler.GetTransactions(currency, startDate, endDate, isValidStatus ? (TransactionStatusId?)statusId : null);
            return View("ShowData", result);
        }

        public IActionResult ShowData()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
