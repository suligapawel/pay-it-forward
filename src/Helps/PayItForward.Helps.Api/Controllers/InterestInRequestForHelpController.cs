using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PayItForward.Helps.Api.Requests;
using PayItForward.Shared.CQRS.Commands.Abstractions;
using PayItForward.Shared.Implementations;
using PayItForward.Shared.Requests;

namespace PayItForward.Helps.Api.Controllers;

internal static class InterestInRequestForHelpController
{
    internal static IApplicationBuilder AddInterestInRequestForHelpController(this WebApplication app)
    {
        app
            .Post();

        return app;
    }

    private static IEndpointRouteBuilder Post(this IEndpointRouteBuilder app)
    {
        app.MapPost("/requests-for-help/{id:Guid}/interests", async
            (
                [FromServices] ICommandDispatcher dispatcher,
                [FromServices] ICurrentUser currentUser,
                [FromBody] ExpressInterest request,
                Guid id) =>
            {
                var command = request.AsCommand(id, currentUser.Id);
                await dispatcher.Execute(command);

                return TypedResults.Ok(id);
            })
            .RequireAuthorization()
            .AddEndpointFilter<RequestValidatorFilter>()
            .WithTags("Interest in request for help")
            .WithOpenApi();

        return app;
    }
}