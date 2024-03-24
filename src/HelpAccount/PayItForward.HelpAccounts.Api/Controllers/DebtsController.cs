using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PayItForward.HelpAccounts.Core.Services;
using PayItForward.Shared.Implementations;

namespace PayItForward.HelpAccounts.Api.Controllers;

internal static class DebtsController
{
    private const string Endpoint = "debts";

    internal static IApplicationBuilder AddDebtsController(this WebApplication app)
    {
        app
            .GetByAccountOwnerId();
            // .GetByCurrentUser();

        return app;
    }

    private static IEndpointRouteBuilder GetByAccountOwnerId(this IEndpointRouteBuilder app)
    {
        app.MapGet("help-accounts/{accountOwnerId:Guid}/debts", async
                ([FromServices] IHelpAccountService service, Guid accountOwnerId)
                => TypedResults.Ok(await service.GetDebt(accountOwnerId)))
            .WithTags("Debts")
            .WithOpenApi();

        return app;
    }

    // private static IEndpointRouteBuilder GetByCurrentUser(this IEndpointRouteBuilder app)
    // {
    //     app.MapGet("help-accounts/debts", async
    //             ([FromServices] IHelpAccountService service, [FromServices] ICurrentUser currentUser)
    //             => TypedResults.Ok(await service.GetDebt(currentUser.Id)))
    //         .WithTags("Debts")
    //         .WithOpenApi();
    //
    //     return app;
    // }
}