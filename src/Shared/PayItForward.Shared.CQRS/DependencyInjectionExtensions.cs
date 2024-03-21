using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PayItForward.Shared.CQRS.CancellationTokens;
using PayItForward.Shared.CQRS.Commands.Abstractions;
using PayItForward.Shared.CQRS.Commands.Implementations;
using PayItForward.Shared.CQRS.Events;
using PayItForward.Shared.CQRS.Events.Abstractions;

namespace PayItForward.Shared.CQRS;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCqrs(this IServiceCollection services, params Assembly[] assemblies)
        => services
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
            .AddSingleton<ICancellationTokenProvider, CancellationTokenProvider>()
            .AddCommands(assemblies)
            .AddEvents(assemblies);

    private static IServiceCollection AddCommands(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        => services
            .AddScoped<ICommandDispatcher, CommandDispatcher>()
            .AddScopedHandlers(typeof(ICommandHandler<>), assemblies);

    public static IServiceCollection AddEvents(this IServiceCollection services, params Assembly[] assemblies)
        => services
            .AddScoped<IEventDispatcher, EventDispatcher>()
            .AddSingleton<IEventMapper, EventMapper>()
            .AddEventDictionary(assemblies)
            .AddScopedHandlers(typeof(IEventHandler<>), assemblies);


    private static IServiceCollection AddScopedHandlers(this IServiceCollection services, Type type, IEnumerable<Assembly> assemblies)
        => services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes
                .AssignableTo(type)
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

    private static IServiceCollection AddEventDictionary(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IEventDictionary, EventDictionary>(_ =>
        {
            var eventDictionary = new EventDictionary();
            assemblies
                .SelectMany(assembly
                    => assembly
                        .GetTypes()
                        .Where(type => type.IsAssignableTo(typeof(IEvent))))
                .ToList()
                .ForEach(eventType => eventDictionary.Register(eventType));

            return eventDictionary;
        });

        return services;
    }
}