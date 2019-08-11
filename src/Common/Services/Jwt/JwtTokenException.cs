using System;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace app.Common.Services.Jwt
{
    public class JwtTokenException : Exception
    {
        public JwtTokenException(string message) : base(message)
        {
        }

        public static JwtTokenException Unauthorized()
        {
            return new JwtTokenException("You are in anonimous session. Make sure you are specifying Authorization header with JWT token in request.");
        }
        
        public static JwtTokenException Expired()
        {
            return new JwtTokenException("Jwt token was expired.");
        }
    }
}