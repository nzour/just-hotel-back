using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using app.Application.Abstraction;
using app.Domain.User;
using Microsoft.IdentityModel.Tokens;

namespace app.Infrastructure.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private JwtSecurityTokenHandler TokenHandler { get; } = new JwtSecurityTokenHandler();
        private Func<User, IEnumerable<Claim>>? ClaimsGenerator { get; set; }
        
        public string CreateToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("TOKEN_SECRET_KEY") ?? "");

            var claims = null != ClaimsGenerator ? ClaimsGenerator.Invoke(user) : CreateDefaultClaims(user);
            
            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = null,
                Audience = null,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddSeconds(Convert.ToDouble(Environment.GetEnvironmentVariable("TOKE_TTL") ?? "")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(claims)
            };

            return TokenHandler.WriteToken(TokenHandler.CreateJwtSecurityToken(descriptor));
        }

        public void ConfigureClaims(Func<User, IEnumerable<Claim>> claimsGenerator)
        {
            ClaimsGenerator = claimsGenerator;
        }

        private IEnumerable<Claim> CreateDefaultClaims(User user)
        {
            return new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("login", user.Login)
            };
        }
    }
}