using PayItForward.HelpAccounts.Shared;
using PayItForward.Helps.Application.Exceptions;
using PayItForward.Helps.Application.ViewModels;
using PayItForward.Helps.Application.ViewModels.Repositories;
using PayItForward.Helps.Domain.Aggregates;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.CQRS.Commands.Abstractions;

namespace PayItForward.Helps.Application.Commands;

public record CreateRequestForHelp(Needy Needy, string Description) : CreateCommand;

internal sealed class CreateRequestForHelpHandler : ICommandHandler<CreateRequestForHelp>
{
    private readonly IRequestForHelpRepository _requestsForHelp;
    private readonly IHelpAccountProxy _helpAccountProxy;
    private readonly IRequestForHelpsViewModelRepository _viewModel;

    public CreateRequestForHelpHandler(
        IRequestForHelpRepository requestsForHelp,
        IHelpAccountProxy helpAccountProxy,
        IRequestForHelpsViewModelRepository viewModel)
    {
        _requestsForHelp = requestsForHelp;
        _helpAccountProxy = helpAccountProxy;
        _viewModel = viewModel;
    }

    public async Task Handle(CreateRequestForHelp command, CancellationToken cancellationToken)
    {
        var requestForHelp = RequestForHelp.New(new RequestForHelpId(command.AggregateId), command.Needy);

        if (!await _helpAccountProxy.CanIncurDebt(command.Needy.Id))
        {
            throw new TheNeedyCannotIncurDebt(command.Needy);
        }

        await _requestsForHelp.Create(requestForHelp, cancellationToken);
        await _helpAccountProxy.IncurDebt(command.Needy.Id);
        await _viewModel.Add(new RequestForHelpViewModel
        {
            Id = requestForHelp.Id.Value,
            Accepted = false,
            Description = command.Description,
            NeedyName = "" // TODO
        }, cancellationToken);
    }
}