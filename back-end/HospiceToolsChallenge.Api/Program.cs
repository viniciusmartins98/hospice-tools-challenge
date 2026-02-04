using HospiceToolsChallenge.Api;
using HospiceToolsChallenge.Api.Middlewares;
using HospiceToolsChallenge.Api.Security.Auth;
using HospiceToolsChallenge.Api.Security.Cors;
using HospiceToolsChallenge.Api.Filters;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers(options => options.AddFilters())
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
        );
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Auth
builder.Services.AddAuth(builder.Configuration);

// Add CORS
builder.Services.AddCors(builder.Configuration);

// Dependency Injection
builder.Services.AddDependencies(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.UseCustomMiddlewares();

app.MapControllers();

app.Run();
