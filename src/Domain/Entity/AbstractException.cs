using System;

namespace app.Domain.Entity
{
    public abstract class AbstractException : Exception
    {
        public AbstractException(string message) : base(message)
        {
        }
    }
}