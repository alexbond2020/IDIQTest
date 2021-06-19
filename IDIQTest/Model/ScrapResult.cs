namespace IDIQTest.Domain.Model
{
    public class ScrapResult
    {
        public ScrapResult(string url, string content)
        {
            Url = url;
            Content = content;
        }

        public ScrapResult(string url, string content, bool hasException)
        {
            Url = url;
            Content = content;
            HasException = hasException;
        }

        public string Url { get; }

        public string Content { get; }

        public bool HasException { get; }

    }
}
