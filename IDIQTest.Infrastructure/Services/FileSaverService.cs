using IDIQTest.Domain.Exceptions;
using IDIQTest.Domain.Model;
using IDIQTest.Domain.Services;
using System;
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

        /// <summary>
        /// Method will create path if it doesn't exists.
        /// Saves scraped content from input parameter to file
        /// </summary>
        /// <param name="result">Object with filename, content</param>
        /// <returns></returns>
        public async Task SaveContentAsync(ScrapResultForFile result)
        {
            try
            {
                if (!Directory.Exists(_baseDirectory))
                {
                    Directory.CreateDirectory(_baseDirectory);
                }

                var fileNamePath = $@"{_baseDirectory}\{result.FileName}";
                await File.WriteAllTextAsync(fileNamePath, result.ScrapResult.Content);
            }
            catch(Exception ex)
            {
                throw new SaveScrapedContentException(ex);
            }
        }
    }
}
