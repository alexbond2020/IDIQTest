using System;

namespace IDIQTest.Domain.Exceptions
{
    /// <summary>
    /// Custom exception type to handle not reacheble Url during scraping process
    /// </summary>
    public class NotAvailableUrlException : Exception
    {
        const string ExceptionMessage = "Url is not reachable";

        public NotAvailableUrlException() : base(ExceptionMessage)
        {
        }

        public NotAvailableUrlException(Exception exception) : base(ExceptionMessage, exception)
        {
        }
       
    }
}
