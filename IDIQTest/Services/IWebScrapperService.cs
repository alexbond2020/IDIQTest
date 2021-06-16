using IDIQTest.Domain.Model;
using System.Threading.Tasks;

namespace IDIQTest.Domain.Services
{
    public interface IWebScrapperService
    {
        Task<ScrapResult> GetContentAsync(string uri);

    }
}
