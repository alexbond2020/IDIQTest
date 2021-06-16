using IDIQTest.Domain.Model;
using IDIQTest.Domain.Services;
using System.IO;
using System.Threading.Tasks;

namespace IDIQTest.Infrastructure.Services
{
    public class FileSaverService : IFileSaverService
    {
        private string _baseDirectory;

        public FileSaverService(string baseDirectory)
        {
            _baseDirectory = baseDirectory;
        }
        public async Task SaveContentAsync(ScrapResult result)
        {
            if (!Directory.Exists(_baseDirectory))
            {
                Directory.CreateDirectory(_baseDirectory);
            }
            var fileNamePath = $@"{_baseDirectory}\{result.FileName}";
            await File.WriteAllTextAsync(fileNamePath, result.Content);            
        }
    }
}
