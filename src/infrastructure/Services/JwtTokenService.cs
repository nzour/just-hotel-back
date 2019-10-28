using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Abstraction;
using Domain.User;
using Domain.UserEntity;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private JwtSecurityTokenHandler TokenHandler { get; } = new JwtSecurityTokenHandler();
        private Func<UserEntity, IEnumerable<Claim>>? ClaimsGenerator { get; set; }

        public string CreateToken(UserEntity userEntity)
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("TOKEN_SECRET_KEY") ?? "");

            var claims = null != ClaimsGenerator ? ClaimsGenerator.Invoke(userEntity) : CreateDefaultClaims(userEntity);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = null,
                Audience = null,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires =
                    DateTime.UtcNow.AddSeconds(
                        Convert.ToDouble(Environment.GetEnvironmentVariable("TOKE_TTL") ?? "")),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(claims)
            };

            return TokenHandler.WriteToken(TokenHandler.CreateJwtSecurityToken(descriptor));
        }

        public void ConfigureClaims(Func<UserEntity, IEnumerable<Claim>> claimsGenerator)
        {
            ClaimsGenerator = claimsGenerator;
        }

        private IEnumerable<Claim> CreateDefaultClaims(UserEntity userEntity)
        {
            return new List<Claim> { new Claim("userId", userEntity.Id.ToString()), new Claim("login", userEntity.Login) };
        }
    }
}