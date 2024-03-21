using PayItForward.Shared.CQRS.Events;
using PayItForward.Shared.CQRS.Events.Exceptions;
using PayItForward.Shared.CQRS.Tests.Fixtures.Commands;
using PayItForward.Shared.CQRS.Tests.Fixtures.Events;
using EventForTestDifferentNamespace = PayItForward.Shared.CQRS.Tests.Fixtures.Events.DifferentNamespace.EventForTest;

namespace PayItForward.Shared.CQRS.Tests;

public sealed class EventDictionaryTests
{
    private EventDictionary _dictionary;

    [SetUp]
    public void Setup()
    {
        _dictionary = new EventDictionary();
    }

    [Test]
    public void Should_add_new_definition_when_event_name_not_exists()
    {
        var typeOfEvent = typeof(EventForTest);

        _dictionary.Register(typeOfEvent);

        var eventTypes = _dictionary.Get(typeOfEvent);
        Assert.That(eventTypes, Has.Count.EqualTo(1));
    }

    [Test]
    public void Should_add_to_definition_when_event_name_is_same_and_definition_is_exists()
    {
        var typeOfEvent = typeof(EventForTestDifferentNamespace);
        _dictionary.Register(typeof(EventForTest));

        _dictionary.Register(typeOfEvent);

        var eventTypes = _dictionary.Get(typeOfEvent);
        Assert.That(eventTypes, Has.Count.EqualTo(2));
    }

    [Test]
    public void Should_throw_when_type_is_not_assignable_to_IEvent()
    {
        Assert.Throws<ArgumentIsNotIEventException>(() => _dictionary.Register(typeof(CommandWithHandler)));
    }

    [Test]
    public void Should_ignore_and_dont_put_event_type_twice_when_type_is_in_definition_already()
    {
        var typeOfEvent = typeof(EventForTest);
        _dictionary.Register(typeOfEvent);

        _dictionary.Register(typeOfEvent);

        var eventTypes = _dictionary.Get(typeOfEvent);

        Assert.That(eventTypes, Is.Not.Empty.And.Count.EqualTo(1));
    }

    [Test]
    public void Should_return_empty_collection_when_type_was_not_registered()
    {
        var typeOfEvent = typeof(EventForTest);

        var eventTypes = _dictionary.Get(typeOfEvent);

        Assert.That(eventTypes, Is.Empty);
    }

    [TearDown]
    public void TearDown()
    {
        _dictionary.Clean();
    }
}