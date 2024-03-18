using PayItForward.HelpAccounts.Core;
using PayItForward.Helps.Api.Controllers;
using PayItForward.Helps.Application;
using PayItForward.Helps.Application.Commands;
using PayItForward.Helps.Infrastructure;
using PayItForward.Shared.CQRS;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCqrs(typeof(CreateRequestForHelp).Assembly);

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddHelpAccounts(); // TODO: remove this reference

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddRequestsForHelpController();

app.Run();