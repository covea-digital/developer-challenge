using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PetInsuranceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly ILogger<QuoteController> _logger;

        public QuoteController(ILogger<QuoteController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public Quotes Post(IEnumerable<Risk> risks)
        {
            return null;
        }
    }
}
