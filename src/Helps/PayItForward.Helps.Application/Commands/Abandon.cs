using PayItForward.Helps.Application.Events;
using PayItForward.Helps.Domain.Events;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.CQRS.Commands.Abstractions;
using PayItForward.Shared.CQRS.Events.Abstractions;
using PayItForward.Shared.Kernel.Helpers;

namespace PayItForward.Helps.Application.Commands;

// TODO: Tests
public record Abandon(ActiveHelpId ActiveHelpId, Helper Helper) : Command;

public class AbandonHandler : ICommandHandler<Abandon>
{
    private readonly IActiveHelpRepository _activeHelps;
    private readonly IClock _clock;
    private readonly IEventDispatcher _dispatcher;

    public AbandonHandler(IActiveHelpRepository activeHelps, IClock clock, IEventDispatcher dispatcher)
    {
        _activeHelps = activeHelps;
        _clock = clock;
        _dispatcher = dispatcher;
    }

    public async Task Handle(Abandon command, CancellationToken cancellationToken)
    {
        var activeHelp = await _activeHelps.Get(command.ActiveHelpId, cancellationToken);

        var @event = activeHelp.Abandon(command.Helper, _clock);

        await _activeHelps.Update(activeHelp, cancellationToken);
        await _dispatcher.Publish(Map(@event));
     }

    private static ActiveHelpWasAbandoned Map(HelpAbandoned @event)
        => new(@event.HelpId.Value, @event.Helper.Id);
}