using ClaimTheSquare.Api.DTOs;
using ClaimTheSquare.Api.Endpoints;
using ClaimTheSquare.Api.Mapping;
using ClaimTheSquare.Application.Interfaces;
using ClaimTheSquare.Application.Services;
using ClaimTheSquare.Domain;
using ClaimTheSquare.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ISquareRepository,InMemorySquareRepository>();
builder.Services.AddScoped<SquareService, SquareService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapSquareEndpoints();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();



app.Run();