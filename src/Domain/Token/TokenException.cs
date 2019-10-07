using System;

namespace app.Domain.Token
{
    public class TokenException : Exception
    {
        public TokenException(string message) : base(message) { }
    }
}