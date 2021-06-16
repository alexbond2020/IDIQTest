using System;

namespace IDIQTest.Domain.Exceptions
{
    public class EmptyUrlExpection : Exception
    {
        public EmptyUrlExpection():base("Url is empty or null") { }
    }
}
