using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PayItForward.Gateway.Identity.Repositories;
using PayItForward.Gateway.Identity.Services;
using PayItForward.Gateway.Identity.Services.Abstraction;
using PayItForward.Gateway.Identity.Settings;

namespace PayItForward.Gateway.Identity;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddOwnIdentity(this IServiceCollection services, IConfiguration config)
    {
        var identitySettings = config.GetSection("gateway:identity").Get<IdentitySettings>();
        services.Configure<IdentitySettings>(config.GetSection("gateway:identity"));

        return services
            .AddAuth(identitySettings)
            .AddSingleton<IPasswordHasher, PasswordHasher>()
            .AddScoped<ITokenService, JwtToken>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IUserRepository, InMemoryUserRepository>();
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, IdentitySettings identitySettings)
    {
        services
            .AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(identitySettings.Tokens.Access.SecretAsBytes()),
                    ValidateIssuer = true,
                    ValidIssuer = identitySettings.Tokens.Issuer,
                    ValidateAudience = true,
                    ValidAudience = identitySettings.Tokens.Audience,
                    ClockSkew = TimeSpan.Zero,
                };

                opt.MapInboundClaims = false;
            });

        services.AddAuthorization();

        return services;
    }
}