using System;

namespace IDIQTest.Domain.Exceptions
{
    /// <summary>
    /// Custom exception type to handle empty of null url during scraping
    /// </summary>
    public class EmptyUrlExpection : Exception
    {
        const string ExceptionMessage = "Url is empty or null";
        public EmptyUrlExpection() : base(ExceptionMessage) 
        {
        }

        public EmptyUrlExpection(Exception exception) : base(ExceptionMessage, exception)
        {
        }
    }
}
