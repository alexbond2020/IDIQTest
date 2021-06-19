using IDIQTest.Domain.Model;
using IDIQTest.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDIQTest.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScrapController : ControllerBase
    {
        private readonly ILogger<ScrapController> _logger;
        private readonly IWebScrapperService _webScraperService;

        public ScrapController(
            IWebScrapperService webScraperService,
            ILogger<ScrapController> logger)
        {
            _webScraperService = webScraperService ?? throw new ArgumentException(nameof(IWebScrapperService));
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

        [HttpGet]
        public async Task<ActionResult<ScrapResult>> GetScrapedUrls(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return BadRequest();
            }
            var result = await _webScraperService.GetContentAsync(url);
            return Ok(result);
        }
    }
}
