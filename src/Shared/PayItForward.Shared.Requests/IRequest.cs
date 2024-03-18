namespace PayItForward.Shared.Requests;

public interface IRequest
{
    string Error { get; }
    bool IsValid();
}