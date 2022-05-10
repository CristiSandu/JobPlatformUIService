
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JobPlatformUIService.Authorization
{
    public static class AuthorizationExtensions
    {
        public static void AddAuth(this WebApplicationBuilder builder)
        {
          
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["IdentityServerAuthenticationOptions:Authority"]; ;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["IdentityServerAuthenticationOptions:Authority"],
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["IdentityServerAuthenticationOptions:Project_id"],
                    ValidateLifetime = true
                };
            });

            string[] AuthSchemes = { JwtBearerDefaults.AuthenticationScheme };
            builder.Services.AddAuthorization(options =>
            {
                options.DefaultPolicy =
                    new AuthorizationPolicyBuilder(AuthSchemes)
                        .RequireAuthenticatedUser()
                        .Build();
            });
        }

        public static void UseAuthenticationAndAuthorization(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
