using System;

namespace IDIQTest.Domain.Exceptions
{
    /// <summary>
    /// Custom exception type to handle an exception during saving content to file process
    /// </summary>
    public class SaveScrapedContentException : Exception
    {
        const string ExceptionMessage = "Saving content to file process has failed";

        public SaveScrapedContentException() : base(ExceptionMessage)
        {
        }

        public SaveScrapedContentException(Exception ex) : base(ExceptionMessage, ex) 
        {
        }
    }
}
