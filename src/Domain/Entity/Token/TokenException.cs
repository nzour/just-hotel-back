using System;

namespace app.Domain.Entity.Token
{
    public class TokenException : Exception
    {
        public TokenException(string message) : base(message) { }
    }
}