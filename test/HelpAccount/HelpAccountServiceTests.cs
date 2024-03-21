using PayItForward.Debts.Core.Tests.Fixtures;
using PayItForward.HelpAccounts.Core.Exceptions;
using PayItForward.HelpAccounts.Core.Repositories;
using PayItForward.HelpAccounts.Core.Services;
using PayItForward.Shared.CQRS.CancellationTokens;

namespace PayItForward.Debts.Core.Tests;

public class HelpAccountServiceTests
{
    private static readonly Guid AccountOwnerId = Guid.Parse("566d3130-7986-4fcd-9fba-b675e38bf478");
    private IHelpAccountsRepository _helpAccountsInMemoryRepository;
    private IHelpAccountService _service;
    private CancellationTokenProviderForTests _cancellationTokenProvider;

    [SetUp]
    public void SetUp()
    {
        _helpAccountsInMemoryRepository = new HelpAccountsForTestsRepository();
        _cancellationTokenProvider = new CancellationTokenProviderForTests();
        _service = new HelpAccountService(_helpAccountsInMemoryRepository, _cancellationTokenProvider);
    }

    [Test]
    public async Task Should_incur_debt()
    {
        await _service.IncurDebt(AccountOwnerId);

        var result = await _helpAccountsInMemoryRepository.Get(AccountOwnerId, _cancellationTokenProvider.CreateToken());
        Assert.That(result.Value, Is.EqualTo(-3));
    }

    [Test]
    public void Should_not_incur_debt_when_it_does_not_exist()
    {
        Assert.ThrowsAsync<NotFound>(() => _service.IncurDebt(Guid.NewGuid()));
    }

    [Test]
    public async Task Should_pay_off_debt()
    {
        await _service.PayOffDebt(AccountOwnerId);

        var result = await _helpAccountsInMemoryRepository.Get(AccountOwnerId,  _cancellationTokenProvider.CreateToken());
        Assert.That(result.Value, Is.EqualTo(1));
    }

    [Test]
    public void Should_not_pay_off_debt_when_it_does_not_exist()
    {
        Assert.ThrowsAsync<NotFound>(() => _service.PayOffDebt(Guid.NewGuid()));
    }

    [Test]
    public async Task Should_can_incur_debt_when_help_account_value_is_not_less_than_0()
    {
        var result = await _service.CanIncurDebt(AccountOwnerId);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task Should_cannot_incur_debt_when_help_account_value_is_less_than_0()
    {
        await _service.IncurDebt(AccountOwnerId);

        var result = await _service.CanIncurDebt(AccountOwnerId);

        Assert.That(result, Is.False);
    }

    [Test]
    public void Should_throw_not_found_when_help_account_does_not_exist()
    {
        Assert.ThrowsAsync<NotFound>(() => _service.CanIncurDebt(Guid.NewGuid()));
    }
}