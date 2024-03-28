using PayItForward.Helps.Application.ViewModels;
using PayItForward.Helps.Application.ViewModels.Repositories;

namespace PayItForward.Helps.Infrastructure.Repositories;

internal class InMemoryRequestForHelpsViewModelRepository : IRequestForHelpsViewModelRepository
{
    private static readonly Dictionary<Guid, RequestForHelpViewModel> RequestsForHelp = new();

    public Task<IReadOnlyCollection<RequestForHelpViewModel>> Get(CancellationToken cancellationToken)
        => Task.FromResult<IReadOnlyCollection<RequestForHelpViewModel>>(RequestsForHelp.Values.ToList());

    public Task<RequestForHelpViewModel> GetDetails(Guid id, CancellationToken cancellationToken)
        => Task.FromResult(RequestsForHelp.GetValueOrDefault(id));

    public Task Add(RequestForHelpViewModel requestForHelp, CancellationToken cancellationToken)
    {
        RequestsForHelp.Add(requestForHelp.Id, requestForHelp);
        return Task.CompletedTask;
    }

    public Task AssignPotentialHelper(Guid id, PotentialHelperViewModel potentialHelper, CancellationToken cancellationToken)
    {
        var requestForHelp = RequestsForHelp.GetValueOrDefault(id);

        requestForHelp.PotentialHelpers.Add(potentialHelper);

        return Task.CompletedTask;
    }

    public Task RemovePotentialHelper(Guid id, Guid potentialHelperId, CancellationToken cancellationToken)
    {
        var requestForHelp = RequestsForHelp.GetValueOrDefault(id);

        requestForHelp.PotentialHelpers.RemoveAll(x => x.Id == potentialHelperId);

        return Task.CompletedTask;
    }
}