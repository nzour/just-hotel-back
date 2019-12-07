using System;

namespace Application.Exception
{
    public class ValidationException : AggregateException
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}