using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using app.Domain.Entity.Token;
using app.Domain.Entity.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace app.Common.Services.Jwt
{
    public class JwtTokenManager
    {
        private const string AuthorizationHeader = "Authorization";
        private const string TokenPattern = @"Bearer\s(?<token>.*)\s?";
        private static readonly string TokenTtl = Environment.GetEnvironmentVariable("TOKEN_TTL");
        private static readonly string TokenSecret = Environment.GetEnvironmentVariable("TOKEN_SECRET_KEY");

        private ITokenRepository TokenRepository { get; }
        private IHttpContextAccessor ContextAccessor { get; }
        private Regex Regex { get; }

        public JwtTokenManager(ITokenRepository tokenRepository, IHttpContextAccessor contextAccessor)
        {
            TokenRepository = tokenRepository;
            ContextAccessor = contextAccessor;
            Regex = new Regex(TokenPattern);
        }

        public User GetCurrentUser()
        {
            var token = TokenRepository.FindToken(GetHeaderToken());

            return false == token?.IsExpired()
                ? token.User
                : throw JwtTokenException.Expired();
        }

        private string GetHeaderToken()
        {
            StringValues value;

            try
            {
                var hasHeader = ContextAccessor.HttpContext.Request.Headers.TryGetValue(AuthorizationHeader, out value);

                if (false == hasHeader)
                {
                    throw JwtTokenException.Unauthorized();
                }
            }
            catch (ArgumentNullException)
            {
                throw JwtTokenException.Unauthorized();
            }

            var match = Regex.Match(value.ToString());

            return match.Success
                ? match.Groups["token"].Value
                : throw JwtTokenException.Unauthorized();
        }

        public Token CreateAccessTokenForUser(User user)
        {
            var payload = EncodeHandler.Base64Encode(user);
            var signature = SHA256.Create(TokenSecret + DateTime.Now).ToString();

            var token = new Token(
                user, $"{payload}.{signature}", DateTime.Now.AddSeconds(Convert.ToDouble(TokenTtl))
            );
            
            TokenRepository.Create(token);

            return token;
        }
    }
}