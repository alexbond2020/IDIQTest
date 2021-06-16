using System;

namespace IDIQTest.Domain.Exceptions
{
    /// <summary>
    /// Custom exception type to handle wrong format of Url during scraping process
    /// </summary>
    public class UrlFormatExpection : Exception
    {
        const string ExceptionMessage = "Url format is wrong";

        public UrlFormatExpection() : base(ExceptionMessage) 
        {
        }

        public UrlFormatExpection(Exception exception) : base(ExceptionMessage, exception)
        {
        }
    }
}
