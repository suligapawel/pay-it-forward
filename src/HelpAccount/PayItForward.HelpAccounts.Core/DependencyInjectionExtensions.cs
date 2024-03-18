using Microsoft.Extensions.DependencyInjection;
using PayItForward.HelpAccounts.Core.Repositories;
using PayItForward.HelpAccounts.Core.Services;
using PayItForward.HelpAccounts.Shared;

namespace PayItForward.HelpAccounts.Core;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddHelpAccounts(this IServiceCollection services)
        => services
            .AddScoped<IHelpAccountsRepository, HelpAccountsInMemoryRepository>()
            .AddScoped<IHelpAccountService, HelpAccountService>()
            .AddScoped<IHelpAccountProxy, HelpAccountService>();
}