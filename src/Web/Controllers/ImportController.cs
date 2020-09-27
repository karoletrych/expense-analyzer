using System.Threading.Tasks;
using ExpenseAnalyzer.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportController : ControllerBase
    {
        private readonly ILogger<ImportController> _logger;
        private readonly ITransactionParser _transactionParser;

        public ImportController(ILogger<ImportController> logger, ITransactionParser transactionParser)
        {
            _logger = logger;
            _transactionParser = transactionParser;
        }


        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile formFile)
        {
            var stream = formFile.OpenReadStream();
            await _transactionParser.Parse(stream);

            return Created(nameof(ImportController), null);
        }
    }
}
