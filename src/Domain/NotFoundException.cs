using System;

namespace app.Domain
{
    public abstract class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}