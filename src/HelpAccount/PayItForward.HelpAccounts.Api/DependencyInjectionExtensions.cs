using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PayItForward.HelpAccounts.Api.Controllers;
using PayItForward.HelpAccounts.Core;

[assembly: InternalsVisibleTo("PayItForward.Gateway.Api")]

namespace PayItForward.HelpAccounts.Api;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddHelpAccounts(this IServiceCollection services) => services
        .AddCore();

    public static IApplicationBuilder UseHelpAccounts(this WebApplication app)
        => app.AddDebtsController();
}