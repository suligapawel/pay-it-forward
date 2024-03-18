using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("PayItForward.Helps.Api")]

namespace PayItForward.Helps.Application;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services) => services;
}