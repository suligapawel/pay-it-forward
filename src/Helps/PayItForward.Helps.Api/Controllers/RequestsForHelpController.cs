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
            .Post();

        return app;
    }

    private static IEndpointRouteBuilder Post(this IEndpointRouteBuilder app)
    {
        app.MapPost("/requests-for-help", async
            (
                [FromServices] ICommandDispatcher dispatcher,
                [FromServices] ICurrentUser currentUser,
                [FromBody] CreateRequestForHelp createRequestForHelp) =>
            {
                var command = createRequestForHelp.AsCommand(currentUser.Id);
                await dispatcher.Execute(command);

                return TypedResults.Ok(command.AggregateId);
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