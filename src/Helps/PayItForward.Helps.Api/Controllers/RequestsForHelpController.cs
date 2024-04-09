using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PayItForward.Helps.Api.Requests;
using PayItForward.Helps.Application.ViewModels.Repositories;
using PayItForward.Shared.CQRS.Commands.Abstractions;
using PayItForward.Shared.Implementations;
using PayItForward.Shared.Implementations.CancellationTokens;
using PayItForward.Shared.Requests;

namespace PayItForward.Helps.Api.Controllers;

internal static class RequestsForHelpController
{
    private const string Endpoint = "requests-for-help";

    internal static IApplicationBuilder AddRequestsForHelpController(this WebApplication app)
    {
        app
            .GetDetails()
            .CreateRequestForHelp()
            .AcceptPotentialHelper();

        return app;
    }

    private static IEndpointRouteBuilder CreateRequestForHelp(this IEndpointRouteBuilder app)
    {
        app.MapPost("/requests-for-help", async
            (
                [FromServices] ICommandDispatcher dispatcher,
                [FromServices] ICurrentUser currentUser,
                [FromBody] CreateRequestForHelp request) =>
            {
                var command = request.AsCommand(currentUser.Id);
                await dispatcher.Execute(command);

                return TypedResults.Ok(command.AggregateId);
            })
            .RequireAuthorization()
            .AddEndpointFilter<RequestValidatorFilter>()
            .WithTags("Requests for help")
            .WithOpenApi();

        return app;
    }

    private static IEndpointRouteBuilder AcceptPotentialHelper(this IEndpointRouteBuilder app)
    {
        app.MapPost("/requests-for-help/{id:Guid}/accepts", async
            (
                [FromServices] ICommandDispatcher dispatcher,
                [FromServices] ICurrentUser currentUser,
                Guid id,
                [FromBody] AcceptPotentialHelper request) =>
            {
                var command = request.AsCommand(id, currentUser.Id);
                await dispatcher.Execute(command);

                return TypedResults.Ok(id);
            })
            .RequireAuthorization()
            .AddEndpointFilter<RequestValidatorFilter>()
            .WithTags("Requests for help")
            .WithOpenApi();

        return app;
    }


    private static IEndpointRouteBuilder GetDetails(this IEndpointRouteBuilder app)
    {
        app.MapGet("/requests-for-help/{id:Guid}", async
            (
                [FromServices] IRequestForHelpsViewModelRepository viewModels,
                [FromServices] ICancellationTokenProvider cancellationToken,
                Guid id) => TypedResults.Ok(await viewModels.GetDetails(id, cancellationToken.CreateToken())))
            .RequireAuthorization()
            .AddEndpointFilter<RequestValidatorFilter>()
            .WithTags("Requests for help")
            .WithOpenApi();

        return app;
    }
}