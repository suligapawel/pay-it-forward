using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PayItForward.Gateway.Identity.API.Requests;
using PayItForward.Gateway.Identity.Models;
using PayItForward.Gateway.Identity.Services.Abstraction;
using PayItForward.Shared.Implementations.CancellationTokens;
using PayItForward.Shared.Requests;

namespace PayItForward.Gateway.Identity.API.Controllers;

internal static class UsersController
{
    private const string Endpoint = "users";

    internal static IApplicationBuilder AddUsersController(this WebApplication app)
    {
        app
            .SignUp()
            .SignIn()
            .Refresh();

        return app;
    }

    private static IEndpointRouteBuilder SignUp(this IEndpointRouteBuilder app)
    {
        app.MapPost("/sign-up", async
            (
                [FromServices] IUserService userService,
                [FromServices] ICancellationTokenProvider cancellationToken,
                [FromBody] SignUp request) =>
            {
                await userService.SignUp(request.Email, request.Name, request.Password, cancellationToken.CreateToken());
            })
            .AddEndpointFilter<RequestValidatorFilter>()
            .WithTags("Users")
            .WithOpenApi();

        return app;
    }

    private static IEndpointRouteBuilder SignIn(this IEndpointRouteBuilder app)
    {
        app.MapPost("/sign-in", async
            (
                [FromServices] IUserService userService,
                [FromServices] ICancellationTokenProvider cancellationToken,
                [FromBody] SignIn request) =>
            {
                var tokens = await userService.SignIn(request.Email, request.Password, cancellationToken.CreateToken());
                return Results.Ok(tokens);
            })
            .AddEndpointFilter<RequestValidatorFilter>()
            .WithTags("Users")
            .WithOpenApi();

        return app;
    }

    private static IEndpointRouteBuilder Refresh(this IEndpointRouteBuilder app)
    {
        app.MapPost("/refresh", async
            (
                [FromServices] IUserService userService,
                [FromServices] ICancellationTokenProvider cancellationToken,
                [FromBody] Refresh request) =>
            {
                var tokens = await userService.Refresh(new OldToken(request.Token), cancellationToken.CreateToken());
                return Results.Ok(tokens);
            })
            .AddEndpointFilter<RequestValidatorFilter>()
            .WithTags("Users")
            .WithOpenApi();

        return app;
    }
}