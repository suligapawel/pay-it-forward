using PayItForward.Helps.Application.Exceptions;
using PayItForward.Helps.Application.ViewModels.Repositories;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.CQRS.Commands.Abstractions;

namespace PayItForward.Helps.Application.Commands;

public record DoNotHelp(RequestForHelpId RequestForHelpId, PotentialHelper PotentialHelper) : Command;

internal sealed class DoNotHelpHandler : ICommandHandler<DoNotHelp>
{
    private readonly IRequestForHelpRepository _requestsForHelp;
    private readonly IRequestForHelpsViewModelRepository _viewModel;

    public DoNotHelpHandler(IRequestForHelpRepository requestsForHelp,
        IRequestForHelpsViewModelRepository viewModel)
    {
        _requestsForHelp = requestsForHelp;
        _viewModel = viewModel;
    }

    public async Task Handle(DoNotHelp command, CancellationToken cancellationToken)
    {
        var requestForHelp = await _requestsForHelp.Get(command.RequestForHelpId, cancellationToken);

        if (requestForHelp is null)
        {
            throw new RequestForHelpDoesNotExist(command.RequestForHelpId);
        }

        requestForHelp.DoNotHelp(command.PotentialHelper);

        await _requestsForHelp.Update(requestForHelp, cancellationToken);
        await _viewModel.RemovePotentialHelper(command.RequestForHelpId.Value, command.PotentialHelper.Id, cancellationToken);
        await _viewModel.ToggleAccept(command.RequestForHelpId.Value, false, cancellationToken);
    }
}