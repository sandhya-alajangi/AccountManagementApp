using AccountManagementApp.Domain.Contracts;
using AccountManagementApp.Model.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AccountManagementApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeterReadersController : ControllerBase
    {
        private readonly ILogger<MeterReadersController> logger;
        private readonly IMeterReaderService _meterReaderService;

        public MeterReadersController(ILogger<MeterReadersController> logger, IMeterReaderService meterReaderService)
        {
            this.logger = logger;
            this._meterReaderService = meterReaderService;
        }

        /// <summary>
        /// Upload file to process
        /// </summary>
        /// <param name="readingRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResultResponse), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UploadFile([FromForm] ReadingRequest readingRequest)
        {
            ResultResponse resultResponse;
            //Validate
            if (readingRequest == null)
            {
                return BadRequest(new ResultResponse { Success = false, ErrorCode = "S01", Error = "Invalid post request" });
            }

            if (string.IsNullOrEmpty(Request.GetMultipartBoundary()) && readingRequest.File == null)
            {
                return BadRequest(new ResultResponse { Success = false, ErrorCode = "S02", Error = "Invalid post header" });
            }                      
            //Save File
            resultResponse = await _meterReaderService.SaveFileAsync(readingRequest);
            if(resultResponse!=null)
                return BadRequest(resultResponse);

            //Process File
            resultResponse = await _meterReaderService.ProcessFileAsync(readingRequest);

            if (!resultResponse.Success)
            {
                return NotFound(resultResponse);
            }

            return Ok(resultResponse);

        }
    }
}
