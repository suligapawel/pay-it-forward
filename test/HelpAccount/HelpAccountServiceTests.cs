using PayItForward.HelpAccounts.Core.Exceptions;
using PayItForward.HelpAccounts.Core.Repositories;
using PayItForward.HelpAccounts.Core.Services;

namespace PayItForward.Debts.Core.Tests;

public class HelpAccountServiceTests
{
    private static readonly Guid AccountOwnerId = Guid.Parse("566d3130-7986-4fcd-9fba-b675e38bf478");
    private IHelpAccountsRepository _helpAccountsInMemoryRepository;
    private IHelpAccountService _service;

    [SetUp]
    public void SetUp()
    {
        _helpAccountsInMemoryRepository = new HelpAccountsInMemoryRepository();
        _service = new HelpAccountService(_helpAccountsInMemoryRepository);
    }
    
    [Test]
    public async Task Should_incur_debt()
    {
        await _service.IncurDebt(AccountOwnerId);

        var result = await _helpAccountsInMemoryRepository.Get(AccountOwnerId);
        Assert.That(result.Value, Is.EqualTo(-3));
    }

    [Test]
    public void  Should_not_incur_debt_when_it_does_not_exist()
    {
         Assert.ThrowsAsync<NotFound>(() => _service.IncurDebt(Guid.NewGuid()));
    }
}