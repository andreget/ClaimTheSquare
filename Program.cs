using ClaimTheSquare.Api.DTOs;
using ClaimTheSquare.Api.Endpoints;
using ClaimTheSquare.Api.Mapping;
using ClaimTheSquare.Application.Interfaces;
using ClaimTheSquare.Application.Services;
using ClaimTheSquare.Domain;
using ClaimTheSquare.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddSingleton<ISquareRepository,InMemorySquareRepository>();
//builder.Services.AddSingleton<ISquareRepository>(repository =>
//{
//    var repoFilePath = Path.Combine(builder.Environment.ContentRootPath, builder.Configuration.GetValue<string>("SquareRepositoryFilePath") ?? "Data/SquareRepository.json");
//    return new FileSquareRepository(repoFilePath);
//});
builder.Services.AddSingleton<ISquareRepository>(repository =>
{
    var connectionString = builder.Configuration.GetConnectionString("SquareRepositoryDb");

    if(connectionString == null)
    {
        throw new Exception("No connectionstring defined in appsettings.json");
    }
    else
    {
        return new DapperSquareRepository(connectionString);
    }
});

builder.Services.AddScoped<SquareService, SquareService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapSquareEndpoints();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();



app.Run();