namespace PayItForward.Shared.CQRS.Events.Abstractions;

internal interface IEventDictionary
{
    void Register(Type eventType);

    IReadOnlyCollection<Type> Get(Type eventType);

    void Clean();
}