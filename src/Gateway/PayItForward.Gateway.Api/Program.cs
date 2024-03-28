using Microsoft.OpenApi.Models;
using PayItForward.Gateway.Api.Extensions;
using PayItForward.Gateway.Identity;
using PayItForward.HelpAccounts.Api;
using PayItForward.Helps.Api;
using PayItForward.Shared.CQRS;
using PayItForward.Shared.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options
        .AddSwaggerAuth()
        .SwaggerDoc("v1", new OpenApiInfo { Title = "Pay it forward API", Version = "v1" });
});

builder.Services.AddCors();

builder.Services
    .AddOwnIdentity(builder.Configuration)
    .AddHelps()
    .AddHelpAccounts()
    .AddSharedImplementations();

var payItForwardAssemblies = AppDomain.CurrentDomain
    .GetAssemblies()
    .Where(x => x.FullName?.StartsWith("PayItForward") ?? false)
    .ToArray();

builder.Services.AddCqrs(payItForwardAssemblies);
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseUsers();
app.UseHelps();
app.UseHelpAccounts();

app.Run();