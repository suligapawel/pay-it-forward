using PayItForward.Helps.Application.ViewModels;
using PayItForward.Helps.Application.ViewModels.Repositories;

namespace PayItForward.Helps.Application.Tests.Fixtures.Repositories;

public class FakeRequestForHelpViewModelRepository : IRequestForHelpsViewModelRepository
{
    private readonly Dictionary<Guid, RequestForHelpViewModel> _requestsForHelp = new();

    public Task<IReadOnlyCollection<RequestForHelpViewModel>> Get(CancellationToken cancellationToken)
        => Task.FromResult<IReadOnlyCollection<RequestForHelpViewModel>>(_requestsForHelp.Values.ToList());

    public Task<RequestForHelpViewModel> GetDetails(Guid id, CancellationToken cancellationToken)
        => Task.FromResult(_requestsForHelp.GetValueOrDefault(id));

    public Task Add(RequestForHelpViewModel requestForHelp, CancellationToken cancellationToken)
    {
        _requestsForHelp.Add(requestForHelp.Id, requestForHelp);
        return Task.CompletedTask;
    }
}