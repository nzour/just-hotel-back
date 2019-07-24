using System;
using JWT.Algorithms;
using JWT.Builder;

namespace app.Common
{
    public static class EncodeHandler
    {
        private static readonly string TokenSecretKey = Environment.GetEnvironmentVariable("TOKEN_SECRET_KEY");
        private static readonly int TokenTtl = Convert.ToInt32(Environment.GetEnvironmentVariable("TOKEN_TTL"));

        public static string EncodeJwt()
        {
            // todo: refactoring
            return new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(TokenSecretKey)
                .WithVerifySignature(true)
                .Build();
        }

        public static void DecodeJwt()
        {
            
        }

        public static void EncodePassword()
        {
            
        }
    }
}