// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Net;
// using System.Threading.Tasks;
// using ExpenseAnalyzer.Core;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.WebUtilities;
// using Microsoft.Extensions.Logging;
// using Microsoft.Net.Http.Headers;
// using SampleApp.Utilities;

// namespace Web.Controllers
// {
//     [ApiController]
//     [Route("[controller]")]
//     public class ImportController : ControllerBase
//     {
//         private readonly ILogger<ImportController> _logger;
//         private readonly ITransactionParser _transactionParser;

//         public ImportController(ILogger<ImportController> logger, ITransactionParser transactionParser)
//         {
//             _logger = logger;
//             _transactionParser = transactionParser;
//         }


//         //https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-3.1#upload-large-files-with-streaming
//         [HttpPost]
//         public async Task<IActionResult> UploadFile()
//         {
//             if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
//             {
//                 ModelState.AddModelError("File", 
//                     $"The request couldn't be processed (Error 1).");
//                 _logger.LogError("Not a multipart content type: {contentType}", Request.ContentType);

//                 return BadRequest(ModelState);
//             }

//             string boundary = MultipartRequestHelper.GetBoundary(
//                 MediaTypeHeaderValue.Parse(Request.ContentType),
//                 _defaultFormOptions.MultipartBoundaryLengthLimit);
//             var reader = new MultipartReader(boundary, HttpContext.Request.Body);
//             var section = await reader.ReadNextSectionAsync();

//             while (section != null)
//             {
//                 var hasContentDispositionHeader = 
//                     ContentDispositionHeaderValue.TryParse(
//                         section.ContentDisposition, out ContentDispositionHeaderValue contentDisposition);

//                 if (hasContentDispositionHeader)
//                 {
//                     // This check assumes that there's a file
//                     // present without form data. If form data
//                     // is present, this method immediately fails
//                     // and returns the model error.
//                     if (!MultipartRequestHelper
//                         .HasFileContentDisposition(contentDisposition))
//                     {
//                         ModelState.AddModelError("File", 
//                             $"The request couldn't be processed (Error 2).");
//                         _logger.LogError("");

//                         return BadRequest(ModelState);
//                     }
//                     else
//                     {
//                         var trustedFileNameForDisplay = WebUtility.HtmlEncode(
//                                 contentDisposition.FileName.Value);
//                         var trustedFileNameForFileStorage = Path.GetRandomFileName();

//                         // **WARNING!**
//                         // In the following example, the file is saved without
//                         // scanning the file's contents. In most production
//                         // scenarios, an anti-virus/anti-malware scanner API
//                         // is used on the file before making the file available
//                         // for download or for use by other systems. 
//                         // For more information, see the topic that accompanies 
//                         // this sample.

//                         var streamedFileContent = await FileHelpers.ProcessStreamedFile(
//                             section, contentDisposition, ModelState, 
//                             _permittedExtensions, _fileSizeLimit);

//                         if (!ModelState.IsValid)
//                         {
//                             return BadRequest(ModelState);
//                         }

//                         using (var targetStream = System.IO.File.Create(
//                             Path.Combine(_targetFilePath, trustedFileNameForFileStorage)))
//                         {
//                             await targetStream.WriteAsync(streamedFileContent);

//                             _logger.LogInformation(
//                                 "Uploaded file '{TrustedFileNameForDisplay}' saved to " +
//                                 "'{TargetFilePath}' as {TrustedFileNameForFileStorage}", 
//                                 trustedFileNameForDisplay, _targetFilePath, 
//                                 trustedFileNameForFileStorage);
//                         }
//                     }
//                 }

//                 // Drain any remaining section body that hasn't been consumed and
//                 // read the headers for the next section.
//                 section = await reader.ReadNextSectionAsync();
//             }

//             return Created(nameof(ImportController), null);
//         }
//     }
// }
