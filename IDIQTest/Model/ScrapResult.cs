using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IDIQTest.Domain.Model
{
    public class ScrapResult
    {
        public ScrapResult(string url, string content)
        {
            Url = url;
            Content = content;
        }

        public string Url { get; }

        public string Content { get; }

        public string FileName
        {
            get
            {
                var fileName = Regex.Replace(Url, "[^a-zA-Z0-9_]+", "_", RegexOptions.Compiled);
                return $"{fileName}.html";
            }
        }
    }
}
