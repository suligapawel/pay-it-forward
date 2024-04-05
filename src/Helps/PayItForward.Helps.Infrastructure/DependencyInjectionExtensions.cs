using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using PayItForward.Helps.Application.ViewModels.Repositories;
using PayItForward.Helps.Domain.Repositories;
using PayItForward.Helps.Infrastructure.Repositories;

[assembly: InternalsVisibleTo("PayItForward.Helps.Api")]

namespace PayItForward.Helps.Infrastructure;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        => services
            .AddScoped<IRequestForHelpRepository, InMemoryRequestForHelpRepository>()
            .AddScoped<IActiveHelpRepository, InMemoryActiveHelpRepository>()
            .AddScoped<IRequestForHelpsViewModelRepository, InMemoryRequestForHelpsViewModelRepository>();
}