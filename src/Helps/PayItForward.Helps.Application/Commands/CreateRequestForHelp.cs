using PayItForward.Helps.Domain.Aggregates;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.CQRS.Commands.Abstractions;

namespace PayItForward.Helps.Application.Commands;

public record CreateRequestForHelp(Needy Needy) : CreateCommand;

internal sealed class CreateRequestForHelpHandler : ICommandHandler<CreateRequestForHelp>
{
    private readonly IRequestForHelpRepository _requestsForHelp;

    public CreateRequestForHelpHandler(IRequestForHelpRepository requestsForHelp)
    {
        _requestsForHelp = requestsForHelp;
    }

    public async Task Handle(CreateRequestForHelp command, CancellationToken cancellationToken)
    {
        var requestForHelp = RequestForHelp.New(new RequestForHelpId(command.AggregateId), command.Needy);

        await _requestsForHelp.Create(requestForHelp, cancellationToken);
    }
}