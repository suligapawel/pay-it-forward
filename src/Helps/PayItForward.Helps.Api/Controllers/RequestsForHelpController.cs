using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PayItForward.Helps.Api.Requests;
using PayItForward.Shared.CQRS.Commands.Abstractions;
using PayItForward.Shared.Implementations;
using PayItForward.Shared.Requests;

namespace PayItForward.Helps.Api.Controllers;

internal static class RequestsForHelpController
{
    private const string Endpoint = "requests-for-help";

    internal static IApplicationBuilder AddRequestsForHelpController(this WebApplication app)
    {
        app.Post();

        return app;
    }

    private static RouteHandlerBuilder Post(this IEndpointRouteBuilder app)
        => app.MapPost("/requests-for-help", async
            (
                [FromServices] ICommandDispatcher dispatcher,
                [FromServices] ICurrentUser currentUser,
                [FromBody] CreateRequestForHelp createRequestForHelp) =>
            {
                var command = CreateRequestForHelp.AsCommand(currentUser.Id);
                await dispatcher.Execute(command);

                return TypedResults.Ok(command.AggregateId);
            })
            .AddEndpointFilter<RequestValidatorFilter>()
            .WithTags("Requests for help")
            .WithOpenApi();
}