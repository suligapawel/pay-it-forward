using Microsoft.AspNetCore.Mvc;
using PayItForward.Helps.Api.Requests;
using PayItForward.Shared.CQRS.Commands.Abstractions;

namespace PayItForward.Helps.Api.Controllers;

internal static class RequestsForHelpController
{
    private const string Endpoint = "requests-for-help";

    internal static WebApplication AddRequestsForHelpController(this WebApplication app)
    {
        app.Post();

        return app;
    }

    private static RouteHandlerBuilder Post(this IEndpointRouteBuilder app)
        => app.MapPost("/requests-for-help", async
            ([FromServices] ICommandDispatcher dispatcher) => // TODO: Add body
        {
            // TODO: ICurrentUserService
            var command = CreateRequestForHelp.AsCommand(Guid.NewGuid());
            await dispatcher.Execute(command);

            return TypedResults.Ok(command.AggregateId);
        });
}