using System;

namespace IDIQTest.Domain.Exceptions
{
    public class UrlFormatExpection : Exception
    {
        public UrlFormatExpection() : base("Url format is wrong") { }
    }
}
