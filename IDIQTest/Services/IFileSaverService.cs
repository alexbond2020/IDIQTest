using IDIQTest.Domain.Model;
using System.Threading.Tasks;

namespace IDIQTest.Domain.Services
{
    public interface IFileSaverService
    {
        Task SaveContentAsync(ScrapResult result);
    }
}
