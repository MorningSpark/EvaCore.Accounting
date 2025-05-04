using EvaCore.Accounting.Infrastructure;
using EvaCore.Accounting.Application.Extensions;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServicesApplication();

builder.Services.AddInfrastructure("Server=www.eva-core.net;Database=db_accounting;User Id=sa;Password=Aezakami123;Encrypt=False;");
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapOpenApi();

app.MapControllers();

app.Run();
