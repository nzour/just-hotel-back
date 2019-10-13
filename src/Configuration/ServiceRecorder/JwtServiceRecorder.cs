using System;
using System.Text;
using app.Application.Abstraction;
using app.Infrastructure.Services;
using kernel.Abstraction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace app.Configuration.ServiceRecorder
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
                    var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("TOKEN_SECRET_KEY"));
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