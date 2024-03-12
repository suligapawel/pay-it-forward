using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PayItForward.Shared.CQRS.CancellationTokens;
using PayItForward.Shared.CQRS.Commands.Abstractions;
using PayItForward.Shared.CQRS.Commands.Implementations;

namespace PayItForward.Shared.CQRS;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCqrs(this IServiceCollection services, params Assembly[] assemblies)
        => services
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
            .AddSingleton<ICancellationTokenProvider, CancellationTokenProvider>()
            .AddCommands(assemblies);

    private static IServiceCollection AddCommands(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        => services
            .AddScoped<ICommandDispatcher, CommandDispatcher>()
            .AddScopedHandlers(typeof(ICommandHandler<>), assemblies);

    private static IServiceCollection AddScopedHandlers(this IServiceCollection services, Type type, IEnumerable<Assembly> assemblies)
        => services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes
                .AssignableTo(type)
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
}