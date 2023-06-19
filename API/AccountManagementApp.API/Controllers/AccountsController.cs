using System;
using System.Threading.Tasks;
using AccountManagementApp.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using AccountManagementApp.Model;
using Microsoft.AspNetCore.Http;
using AccountManagementApp.Model.Contracts;
using System.IO;
using Newtonsoft.Json;
using AccountManagementApp.Model.Models;
using System.Linq;

namespace AccountManagementApp.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
       
        private readonly IMeterReaderService _userService;

        /// <summary>
        /// The file reader
        /// </summary>
        private readonly IFileReader _fileReader;

        /// <summary>
        /// The file processor
        /// </summary>
        private readonly IFileProcessor _fileProcessor;


        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsController"/> class.        
        /// </summary>
        /// <param name="candidateService">The user service.</param>
        public AccountsController( IMeterReaderService userService, IFileReader fileReader, IFileProcessor fileProcessor)
        {
            _userService = userService;
            _fileReader = fileReader;
            _fileProcessor = fileProcessor;
        }

        /// <summary>
        /// List of Accounts
        /// </summary>       
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Get()
        {
            try
            {
                var response = await _userService.GetAccounts();
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}