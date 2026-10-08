using ClaimTheSquare.Api.DTOs;
using ClaimTheSquare.Api.Mapping;
using ClaimTheSquare.Application.Services;
using ClaimTheSquare.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ClaimTheSquare.Api.Endpoints
{
    public static class SquareEndpoints
    {

        public static IEndpointRouteBuilder MapSquareEndpoints(this IEndpointRouteBuilder app)
        {

            app.MapGet("/api/squares", async (SquareService serv) =>
            {
                var squares = await serv.GetAllAsync();
                var dtoList = squares.Select(s => new SquareDTO
                {
                    Index = s.Index,
                    BackColor = s.BackColor,
                    ForeColor = s.ForeColor,
                    Text = s.Text
                }).ToList();

                return dtoList;
            });

            
            app.MapPost("/api/squares", async (SquareDTO dto, SquareService serv) =>
            {
                ClaimResult claimResult = await serv.ClaimAsync(dto.ToDomain());
                switch(claimResult)
                {
                    case ClaimResult.Success: return Results.Created($"/api/squares/{dto.Index}", dto);
                    case ClaimResult.Invalid: return Results.BadRequest();
                    case ClaimResult.AlreadyTaken: return Results.Conflict();
                    default: return Results.Problem(title: "Unknown Error Occurred");
                }
            });

            return app;
        }

    }
}
