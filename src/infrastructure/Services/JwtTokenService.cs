using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.User;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    public interface IJwtTokenService
    {
        string CreateToken(UserEntity userEntity);
    }

    public class JwtTokenService : IJwtTokenService
    {
        private JwtTokenConfig Config { get; }
        private JwtSecurityTokenHandler TokenHandler { get; } = new JwtSecurityTokenHandler();
        private Func<UserEntity, IEnumerable<Claim>>? ClaimsGenerator { get; set; }

        public JwtTokenService(JwtTokenConfig config)
        {
            Config = config;
        }

        public string CreateToken(UserEntity userEntity)
        {
            var secret = Encoding.UTF8.GetBytes(Config.Secret);
            var claims = null != ClaimsGenerator ? ClaimsGenerator.Invoke(userEntity) : CreateDefaultClaims(userEntity);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = null,
                Audience = null,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(
                    Convert.ToDouble(Config.Ttl)
                ),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };


            return TokenHandler.WriteToken(TokenHandler.CreateJwtSecurityToken(descriptor));
        }

        public void ConfigureClaims(Func<UserEntity, IEnumerable<Claim>> claimsGenerator)
        {
            ClaimsGenerator = claimsGenerator;
        }

        private IEnumerable<Claim> CreateDefaultClaims(UserEntity user)
        {
            return new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("Login", user.Login),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
        }
    }

    public class JwtTokenConfig
    {
        public string Secret { get; set; } = string.Empty;
        public uint Ttl { get; set; }
    }
}