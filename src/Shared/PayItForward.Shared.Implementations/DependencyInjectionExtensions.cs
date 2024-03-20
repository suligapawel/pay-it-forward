using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using PayItForward.Shared.Kernel.Helpers;

[assembly: InternalsVisibleTo("PayItForward.Gateway.Api")]

namespace PayItForward.Shared.Implementations;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddSharedImplementations(this IServiceCollection services)
        => services.AddSingleton<IClock, Clock>();
}