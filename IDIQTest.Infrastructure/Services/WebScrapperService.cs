using IDIQTest.Domain.Exceptions;
using IDIQTest.Domain.Model;
using IDIQTest.Domain.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IDIQTest.Infrastructure.Services
{
    public class WebScrapperService : IWebScrapperService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private ILogger<WebScrapperService> _logger;

        public WebScrapperService(ILogger<WebScrapperService> logger)
        {
            _logger = logger != null ? logger : throw new ArgumentNullException();
        }

        /// <summary>
        /// Validates url, sending request to url, prepares result
        /// </summary>
        /// <param name="url">string Url</param>
        /// <returns>Result of scrapping</returns>
        public async Task<ScrapResult> GetContentAsync(string url)
        {
            var methodName = nameof(GetContentAsync);

            try
            {
                _logger.LogInformation($"{methodName} started");
                ValidateUrl(url);
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var content = await SendUrlRequest(request);
                _logger.LogInformation($"{methodName} finished");
                return new ScrapResult(url, content);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex.GetType()} occured. Message : {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Sends request through HttpClint to Url and returns content.
        /// </summary>
        /// <param name="request">HttpRequest. Method type and Url</param>
        /// <returns>Stirng web content</returns>
        private async Task<string> SendUrlRequest(HttpRequestMessage request)
        {
            try
            {
                var response =  _httpClient.Send(request);
                if (!response.IsSuccessStatusCode)
                {
                    throw new NotAvailableUrlException();
                }
                return await response.Content.ReadAsStringAsync();
            }
            catch(Exception ex)
            {
                throw new NotAvailableUrlException(ex);
            }
        }

        /// <summary>
        /// Validates the url if it is http url, throws an exception depends on the validation error
        /// </summary>
        /// <param name="url">String Url</param>
        private void ValidateUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new EmptyUrlExpection();
            }
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                throw new UrlFormatExpection();
            }
            var uri = new Uri(url);
            if (uri.Scheme != Uri.UriSchemeHttp &&
                uri.Scheme != Uri.UriSchemeHttps)
            {
                throw new UrlFormatExpection();
            }
        }
    }
}
