using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using PayItForward.Shared.Implementations.CancellationTokens;
using PayItForward.Shared.Kernel.Helpers;

[assembly: InternalsVisibleTo("PayItForward.Gateway.Api")]

namespace PayItForward.Shared.Implementations;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddSharedImplementations(this IServiceCollection services)
        => services
            .AddSingleton<IClock, Clock>()
            .AddSingleton<ICancellationTokenProvider, CancellationTokenProvider>()
            .AddScoped<ICurrentUser, CurrentUser>();
}