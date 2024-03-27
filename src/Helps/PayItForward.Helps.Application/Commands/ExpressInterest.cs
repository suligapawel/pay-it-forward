using PayItForward.Gateway.Shared.Proxies;
using PayItForward.Helps.Application.Exceptions;
using PayItForward.Helps.Application.ViewModels;
using PayItForward.Helps.Application.ViewModels.Repositories;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.CQRS.Commands.Abstractions;

namespace PayItForward.Helps.Application.Commands;

public record ExpressInterest(RequestForHelpId RequestForHelpId, PotentialHelper PotentialHelper) : Command;

internal sealed class ExpressInterestHandler : ICommandHandler<ExpressInterest>
{
    private readonly IRequestForHelpRepository _requestsForHelp;
    private readonly IRequestForHelpsViewModelRepository _viewModel;
    private readonly IGetUserNameProxy _getUserName;

    public ExpressInterestHandler(
        IRequestForHelpRepository requestsForHelp,
        IRequestForHelpsViewModelRepository viewModel,
        IGetUserNameProxy getUserName)
    {
        _requestsForHelp = requestsForHelp;
        _viewModel = viewModel;
        _getUserName = getUserName;
    }

    public async Task Handle(ExpressInterest command, CancellationToken cancellationToken)
    {
        var requestForHelp = await _requestsForHelp.Get(command.RequestForHelpId, cancellationToken);

        if (requestForHelp is null)
        {
            throw new RequestForHelpDoesNotExist(command.RequestForHelpId);
        }

        requestForHelp.ExpressInterest(command.PotentialHelper);

        await _requestsForHelp.Update(requestForHelp, cancellationToken);

        var potentialHelperName = await _getUserName.Get(command.PotentialHelper.Id);
        await _viewModel.AssignPotentialHelper(
            command.RequestForHelpId.Value,
            new PotentialHelperViewModel { Id = command.PotentialHelper.Id, Name = potentialHelperName },
            cancellationToken);
    }
}