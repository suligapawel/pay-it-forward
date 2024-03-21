namespace PayItForward.Shared.CQRS.Events.Abstractions;

internal interface IEventMapper
{
    object Deserialize(Type type, byte[] value);
    byte[] Serialize(Type type, object value);
}