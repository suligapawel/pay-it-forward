using Microsoft.Extensions.DependencyInjection;
using PayItForward.HelpAccounts.Core.Services;

namespace PayItForward.HelpAccounts.Core;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
        => services.AddScoped<IHelpAccountService, HelpAccountService>();
}