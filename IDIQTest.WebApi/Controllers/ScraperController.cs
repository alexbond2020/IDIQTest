using IDIQTest.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace IDIQTest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScraperController : ControllerBase
    {
        private readonly IWebScrapperService _webScraperService;
        private readonly ILogger<ScraperController> _logger;

        public ScraperController(ILogger<ScraperController> logger, IWebScrapperService webScraperService)
        {
            _logger = logger != null ? logger : throw new ArgumentNullException();
            _webScraperService = webScraperService != null ? webScraperService : throw new ArgumentNullException();
        }

        [HttpGet]
        [Route("GetContent")]
        public async Task<IActionResult> GetContentAsync(string url)
        {
            try
            {
                var result = await _webScraperService.GetContentAsync(url);
                return Ok(result.Content);
            }
            catch
            {
                return BadRequest();
            }            
        }
    }
}
