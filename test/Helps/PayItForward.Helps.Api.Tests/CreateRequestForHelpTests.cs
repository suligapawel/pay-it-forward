using PayItForward.Helps.Api.Requests;

namespace PayItForward.Helps.Api.Tests;

public class CreateRequestForHelpTests
{
    [Test]
    public void Should_valid()
    {
        var request = new CreateRequestForHelp("This is description");

        var result = request.IsValid();

        Assert.That(result, Is.True);
    }

    [TestCase(" ")]
    [TestCase("")]
    public void Should_not_valid_when_description_is_empty_or_whitespace(string description)
    {
        var request = new CreateRequestForHelp(description);

        var result = request.IsValid();

        Assert.That(result, Is.False);
    }

    [Test]
    public void Should_not_valid_when_description_is_null()
    {
        var request = new CreateRequestForHelp(null);

        var result = request.IsValid();

        Assert.That(result, Is.False);
    }
}