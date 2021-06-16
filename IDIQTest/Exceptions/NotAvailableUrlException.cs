using System;

namespace IDIQTest.Domain.Exceptions
{
    public class NotAvailableUrlException : Exception
    {
        public NotAvailableUrlException() : base("Url is not reachable") { }
    }
}
