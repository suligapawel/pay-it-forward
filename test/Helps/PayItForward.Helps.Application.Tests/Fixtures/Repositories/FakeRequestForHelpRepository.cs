using PayItForward.Helps.Domain.Aggregates;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;

namespace PayItForward.Helps.Application.Tests.Fixtures.Repositories;

public class FakeRequestForHelpRepository : IRequestForHelpRepository
{
    public static readonly RequestForHelpId ExistedRequestForHelpId = new(Guid.Parse("a0c37b93-c47f-49f7-ba27-905b6ccba4f7"));
    public static readonly RequestForHelpId NotExistedRequestForHelpId = new(Guid.Parse("b172467e-d2d0-4ce0-9a96-0872ec656ae0"));
    public static readonly Needy Needy = new(Guid.Parse("566d3130-7986-4fcd-9fba-b675e38bf478"));

    private readonly Dictionary<RequestForHelpId, RequestForHelp> _requestsForHelp =
        new() { { ExistedRequestForHelpId, RequestForHelp.New(ExistedRequestForHelpId, Needy) } };

    public Task<RequestForHelp> Get(RequestForHelpId id, CancellationToken cancellationToken)
        => Task.FromResult(_requestsForHelp.GetValueOrDefault(id));

    public Task Create(RequestForHelp requestForHelp, CancellationToken cancellationToken)
    {
        _requestsForHelp.Add(requestForHelp.Id, requestForHelp);
        return Task.CompletedTask;
    }

    public Task Update(RequestForHelp requestForHelp, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task Delete(RequestForHelpId id, CancellationToken cancellationToken)
    {
        _requestsForHelp.Remove(id);
        return Task.CompletedTask;
    }
}