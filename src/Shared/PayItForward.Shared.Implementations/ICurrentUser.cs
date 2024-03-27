using System.Security.Claims;

namespace PayItForward.Shared.Implementations;

public interface ICurrentUser
{
    Guid Id { get; }

    void Initialize(Guid id);
}

public class CurrentUser : ICurrentUser
{
    private bool _initialized = false;
    public Guid Id { get; private set; }

    public void Initialize(Guid id)
    {
        if (_initialized)
        {
            return;
        }

        Id = id;
        _initialized = true;
    }
}