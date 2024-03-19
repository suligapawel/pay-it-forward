using PayItForward.Gateway.Identity.Models;

namespace PayItForward.Gateway.Identity.Services.Abstraction;

internal interface ITokenService
{
    Token GenerateToken(TokenPayload tokenPayload, TokenType tokenType);
    bool ValidToken(string token, TokenType tokenType);
    Guid GetUserId(string token);
}