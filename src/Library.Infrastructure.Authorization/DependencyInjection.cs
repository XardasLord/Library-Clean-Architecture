using System.Text;
using Library.Application.Auth;
using Library.Domain.SharedKernel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Library.Infrastructure.Authorization
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var secret = config.GetSection("JwtConfig").GetSection("Secret").Value;

            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = "localhost",
                        ValidAudience = "localhost"
                    };
                });

            services.AddHttpContextAccessor();

            services.AddTransient<IAuthService, JwtService>();
            services.AddTransient<ICurrentUser, CurrentUser>();

            return services;
        }

        public static IApplicationBuilder UseTokenAuthentication(this IApplicationBuilder app)
            => app.UseAuthentication();

        public static IApplicationBuilder UseTokenAuthorization(this IApplicationBuilder app)
            => app.UseAuthorization();
    }
}
