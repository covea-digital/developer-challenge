using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PetInsuranceApi.CodeLists;

namespace PetInsuranceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodeListController : ControllerBase
    {
        private readonly ILogger<CodeListController> _logger;
        private readonly ICodeListProvider _codeListProvider;

        public CodeListController(ILogger<CodeListController> logger, ICodeListProvider codeListProvider)
        {
            _logger = logger;
            _codeListProvider = codeListProvider;
        }

        [HttpGet]
        [Route("dogs")]
        public IActionResult GetDogs()
        {
            try
            {
                return Ok(_codeListProvider.RetrieveDogs);
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(1), e, "There was a problem getting dog code lists");
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("cats")]
        public IActionResult GetCats()
        {
            try
            {
                return Ok(_codeListProvider.RetrieveCats);
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(1), e, "There was a problem getting cat code lists");
                return StatusCode(500);
            }
        }
    }
}
