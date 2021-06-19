using IDIQTest.Domain.Exceptions;
using IDIQTest.Domain.Services;
using IDIQTest.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace IDIQTest.Infrastructure.Tests.Service
{
    public class WebScrapperServiceTests
    {
        private IWebScrapperService _service;
        private Mock<ILogger<WebScrapperService>> _logger;

        public WebScrapperServiceTests()
        {
            _logger = new Mock<ILogger<WebScrapperService>>();
            _service = new WebScrapperService(_logger.Object);
        }

        [Fact]
        public async Task Should_Throw_EmptyUrlExpection_For_Empty_Url()
        {
            // Act 
            Task act() => _service.GetContentAsync(string.Empty);

            // Assert
            var ex = await Assert.ThrowsAsync<EmptyUrlExpection>(act);
            Assert.Equal("Url is empty or null", ex.Message);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("http")]
        [InlineData("http://")]
        [InlineData("http:/google")]
        [InlineData("http:/google.com")]
        [InlineData("https:/google")]
        [InlineData("https:/google.com")]
        [InlineData("https//:google.com")]
        [InlineData("mailto:name@email.com")]
        [InlineData("ftp://ftp.xyz.com")]
        public async Task Should_Throw_UrlFormatExpection_For_Wrong_Url_Format(string url)
        {
            // Act 
            Task act() => _service.GetContentAsync(url);

            // Assert
            var ex = await Assert.ThrowsAsync<UrlFormatExpection>(act);
            Assert.Equal("Url format is wrong", ex.Message);
        }

        [Theory]
        [InlineData("https://wrong_url.com/")]
        public async Task Should_Throw_WebSiteNotAvailableException_For_Wrong_Url(string url)
        {
            // Act 
            Task act() => _service.GetContentAsync(url);

            // Assert
            var ex = await Assert.ThrowsAsync<NotAvailableUrlException>(act);
            Assert.Equal("Url is not reachable", ex.Message);
        }

        [Fact]
        public async Task Should_Return_String_Content ()
        {
            //Arrange
            var url = @"https://www.w3.org/Provider/Style/dummy.html";

            //Act
            var result = await _service.GetContentAsync(url);

            //Assert
            Assert.False(string.IsNullOrEmpty(result.Content));
        }
    }   
}
