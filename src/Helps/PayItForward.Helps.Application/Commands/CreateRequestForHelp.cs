using PayItForward.HelpAccounts.Shared;
using PayItForward.Helps.Application.Exceptions;
using PayItForward.Helps.Domain.Aggregates;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.CQRS.Commands.Abstractions;

namespace PayItForward.Helps.Application.Commands;

public record CreateRequestForHelp(Needy Needy) : CreateCommand;

internal sealed class CreateRequestForHelpHandler : ICommandHandler<CreateRequestForHelp>
{
    private readonly IRequestForHelpRepository _requestsForHelp;
    private readonly IHelpAccountProxy _helpAccountProxy;

    public CreateRequestForHelpHandler(IRequestForHelpRepository requestsForHelp, IHelpAccountProxy helpAccountProxy)
    {
        _requestsForHelp = requestsForHelp;
        _helpAccountProxy = helpAccountProxy;
    }

    public async Task Handle(CreateRequestForHelp command, CancellationToken cancellationToken)
    {
        var requestForHelp = RequestForHelp.New(new RequestForHelpId(command.AggregateId), command.Needy);

        if (!await _helpAccountProxy.CanIncurDebt(command.Needy.Id))
        {
            throw new TheNeedyCannotIncurDebt(command.Needy);
        }
        
        await _requestsForHelp.Create(requestForHelp, cancellationToken);
    }
}