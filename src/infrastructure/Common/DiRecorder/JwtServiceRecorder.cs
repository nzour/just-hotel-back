using System.Text;
using Application.Abstraction;
using Infrastructure.Services;
using Kernel.Abstraction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Common.DiRecorder
{
    public class JwtServiceRecorder : AbstractServiceRecorder
    {
        public IConfiguration Configuration { get; }

        public JwtServiceRecorder(IConfiguration configuration)
        {
            Configuration = configuration;
        }

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
                    var key = Encoding.UTF8.GetBytes(Configuration["TOKEN_SECRET_KEY"] ?? "");
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