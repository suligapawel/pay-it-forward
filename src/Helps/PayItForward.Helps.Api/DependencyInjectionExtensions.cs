using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PayItForward.Helps.Api.Controllers;
using PayItForward.Helps.Application;
using PayItForward.Helps.Infrastructure;

[assembly: InternalsVisibleTo("PayItForward.Gateway.Api")]

namespace PayItForward.Helps.Api;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddHelps(this IServiceCollection services) => services
        .AddApplication()
        .AddInfrastructure();

    public static IApplicationBuilder UseHelps(this WebApplication app)
    {
        app.AddRequestsForHelpController();
        app.AddInterestInRequestForHelpController();
        app.AddHelpsController();

        return app;
    }
}