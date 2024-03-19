using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using PayItForward.Gateway.Identity.Models;
using PayItForward.Gateway.Identity.Services.Abstraction;
using PayItForward.Gateway.Identity.Settings;
using PayItForward.Shared.Kernel.Helpers;

namespace PayItForward.Gateway.Identity.Services;

internal sealed class JwtToken : ITokenService
{
    private readonly IClock _clock;
    private readonly TokensSettings _settings;

    public JwtToken(IOptions<IdentitySettings> settings, IClock clock)
    {
        _clock = clock;
        _settings = settings.Value.Tokens;
    }

    public Token GenerateToken(TokenPayload tokenPayload, TokenType tokenType)
    {
        ArgumentNullException.ThrowIfNull(tokenPayload);

        var tokenFactory = new TokenFactory(tokenType);
        var tokenSettings = tokenFactory.GetSettings(_settings);
        var payload = tokenFactory.CreatePayload(tokenPayload);
        var key = new SymmetricSecurityKey(tokenSettings.SecretAsBytes());
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var expiresIn = _clock.Now.AddMinutes(tokenSettings.ExpirationTime);
        var token = new SecurityTokenDescriptor
        {
            Claims = payload,
            Expires = expiresIn,
            SigningCredentials = credentials,
            Issuer = _settings.Issuer,
            Audience = _settings.Audience
        };

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.CreateToken(token);
        var tokenValue = handler.WriteToken(jwt);

        return new Token
        {
            Value = tokenValue, 
            ExpiresIn = expiresIn
        };
    }

    public bool ValidToken(string token, TokenType tokenType)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenFactory = new TokenFactory(tokenType);
        var tokenSettings = tokenFactory.GetSettings(_settings);
        var key = new SymmetricSecurityKey(tokenSettings.SecretAsBytes());

        IdentityModelEventSource.ShowPII = true;
        try
        {
            tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = _settings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _settings.Audience,
                    ClockSkew = TimeSpan.Zero,
                },
                out _);
        }
        catch (Exception ex) when (ex is SecurityTokenExpiredException or SecurityTokenSignatureKeyNotFoundException or ArgumentException)
        {
            return false;
        }

        return true;
    }

    public Guid GetUserId(string token)
    {
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        return Guid.Parse(jwt.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value);
    }
}