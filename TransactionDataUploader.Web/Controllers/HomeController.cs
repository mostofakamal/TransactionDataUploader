using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
                try
                {
                    var transactionFile = transactionDataModel.File;
                    if (transactionFile == null || transactionFile.Length == 0)
                    {
                        return Content("File not selected");
                    }

                    var fileReadResult = await FileUtility.ReadFormFileAsync(transactionFile);
                    var errors =
                        await _transactionDataHandler.ParseFileContentAndSaveData(fileReadResult.Content,
                            fileReadResult.FileType);
                    if (errors.Any())
                    {
                        var model = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = string.Join(",",errors) };
                        return View("Error", model);
                    }

                    return View("Index", new TransactionDataFileModel());
                }
                catch(Exception ex)
                {
                    var model = new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = ex.Message};
                    return View("Error", model);
                }
            }
            return View(nameof(Index), transactionDataModel);

        }

        public IActionResult ShowData()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
