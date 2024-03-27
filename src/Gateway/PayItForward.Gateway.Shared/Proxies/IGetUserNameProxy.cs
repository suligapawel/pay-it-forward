namespace PayItForward.Gateway.Shared.Proxies;

public interface IGetUserNameProxy
{
    Task<string> Get(Guid id);
}