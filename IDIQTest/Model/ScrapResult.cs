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
    }
}
