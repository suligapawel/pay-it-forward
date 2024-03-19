using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using PayItForward.Gateway.Identity.Services.Abstraction;
using PayItForward.Gateway.Identity.Settings;

namespace PayItForward.Gateway.Identity.Services;

internal sealed class PasswordHasher : IPasswordHasher
{
    private readonly PasswordSettings _passwordSettings;

    public PasswordHasher(IOptions<PasswordSettings> passwordSettings)
    {
        _passwordSettings = passwordSettings.Value;
    }

    public byte[] ComputeHash(byte[] password, byte[] salt)
        => ComputeHash(password, salt, _passwordSettings.Iterations);

    private byte[] ComputeHash(byte[] password, byte[] salt, int iteration)
    {
        if (iteration <= 0)
        {
            return password;
        }

        using var sha = SHA512.Create();
        var saltedPassword = $"{Convert.ToBase64String(salt)}{Convert.ToBase64String(password)}{_passwordSettings.Pepper}";
        var passwordInBytes = Encoding.UTF8.GetBytes(saltedPassword);
        var hash = sha.ComputeHash(passwordInBytes);

        return ComputeHash(hash, salt, iteration - 1);
    }

    public byte[] GenerateSalt()
    {
        var salt = new byte[_passwordSettings.SaltLength];
        RandomNumberGenerator.Create().GetBytes(salt);

        return salt;
    }
}