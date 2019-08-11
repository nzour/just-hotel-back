using System;
using app.Common.Extensions;
using app.Domain.Entity.Token;
using app.Domain.Entity.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace app.Common.Services.Jwt
{
    public class JwtTokenManager
    {
        private const string AuthorizationHeader = "Authorization";

        private ITokenRepository TokenRepository { get; }
        private IUserRepository UserRepository { get; }
        private IHttpContextAccessor ContextAccessor { get; }

        public JwtTokenManager(ITokenRepository tokenRepository, IUserRepository userRepository,
            IHttpContextAccessor contextAccessor)
        {
            TokenRepository = tokenRepository;
            UserRepository = userRepository;
            ContextAccessor = contextAccessor;
        }

        public void ValidateToken()
        {

        }

        public User GetCurrentUser()
        {
            var token = TokenRepository.FindToken(GetHeaderToken());

            return token.IsNotNull() && !token.IsExpired()
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

            // handle with regex
            return value.ToString();
        }

        public void CreateTokenForUser(User user)
        {
            var secret = Environment.GetEnvironmentVariable("TOKEN_SECRET_KEY");
            var ttl = Convert.ToInt32(Environment.GetEnvironmentVariable("TOKEN_TTL"));
        }
    }
}