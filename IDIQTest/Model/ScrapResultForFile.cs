using System.Text.RegularExpressions;

namespace IDIQTest.Domain.Model
{
    public class ScrapResultForFile
    {
        public ScrapResultForFile(ScrapResult scrapResult) 
        {
            ScrapResult = scrapResult;
        }

        public ScrapResult ScrapResult { get; }

        public string FileName
        {
            get
            {
                var fileName = Regex.Replace(ScrapResult.Url, "[^a-zA-Z0-9_]+", "_", RegexOptions.Compiled);
                return $"{fileName}.html";
            }
        }
    }
}
