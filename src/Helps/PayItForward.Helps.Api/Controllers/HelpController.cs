using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PayItForward.Helps.Application.Commands;
using PayItForward.Helps.Domain.ValueObjects;
using PayItForward.Shared.CQRS.Commands.Abstractions;
using PayItForward.Shared.Implementations;

namespace PayItForward.Helps.Api.Controllers;

internal static class HelpController
{
    private const string Endpoint = "helps";

    internal static IApplicationBuilder AddHelpsController(this WebApplication app)
    {
        app
            .Complete()
            .Abandon();

        return app;
    }

    private static IEndpointRouteBuilder Complete(this IEndpointRouteBuilder app)
    {
        app.MapPost("/helps/{id:Guid}", async
            (
                [FromServices] ICommandDispatcher dispatcher,
                [FromServices] ICurrentUser currentUser,
                Guid id) =>
            {
                var command = new Complete(new ActiveHelpId(id), new Helper(currentUser.Id));
                await dispatcher.Execute(command);

                return TypedResults.Ok(id);
            })
            .RequireAuthorization()
            .WithTags("Helps")
            .WithOpenApi();

        return app;
    }

    private static IEndpointRouteBuilder Abandon(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/helps/{id:Guid}", async
            (
                [FromServices] ICommandDispatcher dispatcher,
                [FromServices] ICurrentUser currentUser,
                Guid id) =>
            {
                var command = new Abandon(new ActiveHelpId(id), new Helper(currentUser.Id));
                await dispatcher.Execute(command);

                return TypedResults.Ok(id);
            })
            .RequireAuthorization()
            .WithTags("Helps")
            .WithOpenApi();

        return app;
    }
}