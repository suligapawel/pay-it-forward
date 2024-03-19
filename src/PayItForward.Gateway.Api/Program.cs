using PayItForward.HelpAccounts.Core;
using PayItForward.Helps.Api;
using PayItForward.Shared.CQRS;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddHelps()
    .AddHelpAccounts();

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

app.UseHelps();

app.Run();