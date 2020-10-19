using System.Linq;
using System.Threading.Tasks;
using ExpenseAnalyzer.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var stream = file.OpenReadStream();
            var result = await _transactionParser.Parse(stream);

            return Ok(result.Transactions.Count());
        }
    }
}
