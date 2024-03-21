using System.Text;
using System.Text.Json;
using PayItForward.Shared.CQRS.Events.Abstractions;

namespace PayItForward.Shared.CQRS.Events;

internal class EventMapper : IEventMapper
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public byte[] Serialize(Type type, object value)
        => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, type, Options));

    public object Deserialize(Type type, byte[] value)
        => JsonSerializer.Deserialize(Encoding.UTF8.GetString(value), type, Options);
}