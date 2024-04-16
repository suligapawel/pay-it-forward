using PayItForward.Helps.Application.ViewModels;
using PayItForward.Helps.Application.ViewModels.Repositories;

namespace PayItForward.Helps.Application.Tests.Fixtures.Repositories;

public class FakeRequestForHelpViewModelRepository : IRequestForHelpsViewModelRepository
{
    private readonly Dictionary<Guid, RequestForHelpViewModel> _requestsForHelp =
        new() { { FakeRequestForHelpRepository.ExistedRequestForHelpId.Value, new RequestForHelpViewModel() } };

    public Task<IReadOnlyCollection<RequestForHelpViewModel>> Get(CancellationToken cancellationToken)
        => Task.FromResult<IReadOnlyCollection<RequestForHelpViewModel>>(_requestsForHelp.Values.ToList());

    public Task<RequestForHelpViewModel> GetDetails(Guid id, CancellationToken cancellationToken)
        => Task.FromResult(_requestsForHelp.GetValueOrDefault(id));

    public Task Add(RequestForHelpViewModel requestForHelp, CancellationToken cancellationToken)
    {
        _requestsForHelp.Add(requestForHelp.Id, requestForHelp);
        return Task.CompletedTask;
    }

    public Task ToggleAccept(Guid id, bool accepted, CancellationToken cancellationToken)
    {
        var requestForHelp = _requestsForHelp.GetValueOrDefault(id);

        requestForHelp.Accepted = accepted;

        return Task.CompletedTask;
    }

    public Task AssignPotentialHelper(Guid id, PotentialHelperViewModel potentialHelper, CancellationToken cancellationToken)
    {
        var requestForHelp = _requestsForHelp.GetValueOrDefault(id);

        requestForHelp.PotentialHelpers.Add(potentialHelper);

        return Task.CompletedTask;
    }

    public Task RemovePotentialHelper(Guid id, Guid potentialHelperId, CancellationToken cancellationToken)
    {
        var requestForHelp = _requestsForHelp.GetValueOrDefault(id);

        requestForHelp.PotentialHelpers.RemoveAll(x => x.Id == potentialHelperId);

        return Task.CompletedTask;
    }
}