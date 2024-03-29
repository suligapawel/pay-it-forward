using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using PayItForward.HelpAccounts.Core.Repositories;
using PayItForward.HelpAccounts.Core.Services;
using PayItForward.HelpAccounts.Shared;

[assembly: InternalsVisibleTo("PayItForward.HelpAccounts.Api")]

namespace PayItForward.HelpAccounts.Core;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
        => services
            .AddScoped<IHelpAccountsRepository, HelpAccountsInMemoryRepository>()
            .AddScoped<IHelpAccountService, HelpAccountService>()
            .AddScoped<IHelpAccountProxy, HelpAccountService>();
}