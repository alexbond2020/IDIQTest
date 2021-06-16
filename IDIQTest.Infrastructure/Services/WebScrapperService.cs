using IDIQTest.Domain.Exceptions;
using IDIQTest.Domain.Model;
using IDIQTest.Domain.Services;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IDIQTest.Infrastructure.Services
{
    public class WebScrapperService : IWebScrapperService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<ScrapResult> GetContentAsync(string url)
        {
            ValidateUrl(url);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var content = await SendUrlRequest(request);
            return new ScrapResult(url, content);
        }

        public string GetFileNameByUrl(string url)
        {
            var fileName = Regex.Replace(url, "[^a-zA-Z0-9_]+", "_", RegexOptions.Compiled);
            return $"{fileName}.html";
        }

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
            catch
            {
                throw new NotAvailableUrlException();
            }
        }

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
