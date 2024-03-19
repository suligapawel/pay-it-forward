namespace PayItForward.Gateway.Identity.Services.Abstraction;

internal interface IPasswordHasher
{
    byte[] GenerateSalt();
    byte[] ComputeHash(byte[] password, byte[] salt);
}