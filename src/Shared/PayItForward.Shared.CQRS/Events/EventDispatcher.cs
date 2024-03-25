using PayItForward.Shared.CQRS.Events.Abstractions;
using PayItForward.Shared.Implementations.CancellationTokens;

namespace PayItForward.Shared.CQRS.Events;


internal class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEventMapper _eventMapper;
    private readonly IEventDictionary _eventDictionary;
    private readonly ICancellationTokenProvider _cancellationTokenProvider;

    public EventDispatcher(
        IServiceProvider serviceProvider,
        IEventMapper eventMapper,
        IEventDictionary eventDictionary,
        ICancellationTokenProvider cancellationTokenProvider)
    {
        _serviceProvider = serviceProvider;
        _eventMapper = eventMapper;
        _eventDictionary = eventDictionary;
        _cancellationTokenProvider = cancellationTokenProvider;
    }

    public async Task Publish<TEvent>(TEvent @event)
        where TEvent : IEvent
    {
        ArgumentNullException.ThrowIfNull(@event);

        var eventsFromDifferentAssemblies = _eventDictionary.Get(@event.GetType());

        if (!eventsFromDifferentAssemblies.Any())
        {
            return;
        }

        var serializedEvent = _eventMapper.Serialize(@event.GetType(), @event);
        var cancellationToken = _cancellationTokenProvider.CreateToken();

        var tasks = eventsFromDifferentAssemblies
            .Select(typeOfEvent =>
            {
                var eventHandlerType = typeof(IEventHandler<>).MakeGenericType(typeOfEvent);
                var handler = _serviceProvider.GetService(eventHandlerType);

                if (handler is null)
                {
                    return Task.CompletedTask;
                }

                return (Task)eventHandlerType
                    .GetMethod(nameof(IEventHandler<TEvent>.Handle))
                    ?.Invoke(
                        handler,
                        new[] { _eventMapper.Deserialize(typeOfEvent, serializedEvent), cancellationToken });
            })
            .ToList();

        await Task.WhenAll(tasks);
    }
}