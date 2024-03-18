using Microsoft.AspNetCore.Mvc;
using PayItForward.Helps.Api.Requests;
using PayItForward.Shared.CQRS.Commands.Abstractions;
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
                [FromBody] CreateRequestForHelp createRequestForHelp) =>
            {
                // TODO: ICurrentUserService
                var command = CreateRequestForHelp.AsCommand(Guid.NewGuid());
                await dispatcher.Execute(command);

                return TypedResults.Ok(command.AggregateId);
            })
            .AddEndpointFilter<RequestValidatorFilter>()
            .WithTags("Requests for help")
            .WithOpenApi();
}