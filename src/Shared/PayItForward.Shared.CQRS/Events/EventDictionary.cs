using PayItForward.Shared.CQRS.Events.Abstractions;
using PayItForward.Shared.CQRS.Events.Exceptions;

namespace PayItForward.Shared.CQRS.Events;

internal sealed class EventDictionary : IEventDictionary
{
    private static readonly Dictionary<string, List<Type>> EventTypes = new();

    public void Register(Type eventType)
    {
        var eventTypeName = eventType.Name;

        ThrowIfTypeIsNotAssignableToIEvent(eventType);

        if (!EventTypes.ContainsKey(eventTypeName))
        {
            EventTypes.Add(eventTypeName, [eventType]);
            return;
        }

        var definition = EventTypes[eventTypeName];

        if (definition.Contains(eventType))
        {
            return;
        }

        EventTypes[eventTypeName].Add(eventType);
    }

    public IReadOnlyCollection<Type> Get(Type eventType)
        => EventTypes.TryGetValue(eventType.Name, out var value) ? value : [];

    public void Clean()
    {
        EventTypes.Clear();
    }

    private static void ThrowIfTypeIsNotAssignableToIEvent(Type eventType)
    {
        if (!eventType.IsAssignableTo(typeof(IEvent)))
        {
            throw new ArgumentIsNotIEventException();
        }
    }
}