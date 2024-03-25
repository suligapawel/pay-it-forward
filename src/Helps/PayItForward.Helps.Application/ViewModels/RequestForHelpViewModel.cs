namespace PayItForward.Helps.Application.ViewModels;

public class RequestForHelpViewModel
{
    public Guid Id { get; init; }
    public string NeedyName { get; init; }
    public string Description { get; init; }
    public bool Accepted { get; init; }
}