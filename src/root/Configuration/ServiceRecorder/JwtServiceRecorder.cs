using System;
using System.Text;
using Application.Abstraction;
using Infrastructure.Services;
using Kernel.Abstraction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Root.Configuration.ServiceRecorder
{
    public class JwtServiceRecorder : AbstractServiceRecorder
    {
        protected override void Execute(IServiceCollection services)
        {
            services.AddTransient<IJwtTokenService, JwtTokenService>();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("TOKEN_SECRET_KEY") ?? "");
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });
        }
    }
}