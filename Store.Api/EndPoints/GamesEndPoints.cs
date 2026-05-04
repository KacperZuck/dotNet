using System;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Store.Api.Data;
using Store.Api.DTO;
using Store.Api.Models;

namespace Store.Api.EndPoints;

public static class GamesEndPoints
{

const string GetGameEndpoint = "GetGame";
private static List<GameSummaryDTO> games = [
    new (1, "Wiedzmin", "Solo", 80.00M, new DateOnly(2000, 1,1)),
    new (2, "Wiedzmin 2", "Solo", 180.00M, new DateOnly(2010, 1,1)),
    new (3, "Wiedzmin 3", "Solo", 280.00M, new DateOnly(2020, 1,1)),

];

public static void MapGamesEndPoints( this WebApplication app)
    {
        var groupGames = app.MapGroup("/games");
        // GET /.. endpoint

        // app.MapGet("/", () => "Hello word , Modyfikacja Kacper");

        // niżej zmieniam HANDLER --> czyli to co dostajesz  () i odsyłasz => ...
        // GET /games endpoint
        groupGames.MapGet("/", async (GameStoreContext dbContext) 
            => await dbContext.Games.Include(game => game.type).Select(game => new GameSummaryDTO(
                    game.id,
                    game.name,
                    game.type!.name,    // ! -- jest dla ignorowania potencjalnego null przez kompilator
                    game.price,
                    game.releaseDate
            )).AsNoTracking().ToListAsync()
        );

        // GET /games/id
        groupGames.MapGet("/{id}", async (int id, GameStoreContext dbContext) => {
            
            var game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(
                new GameDetailsDTO(
                    game.id,
                    game.name,
                    game.typeId,
                    game.price,
                    game.releaseDate
                ));
            }).WithName(GetGameEndpoint); // Getter z nazwą dla innych funkcji do odwołania

        // POST /games -- tworzenie gry ( w ansyc formie )
        groupGames.MapPost("/", async ( CreateGameDTO newGame, GameStoreContext dbContext) =>
        {
            if (string.IsNullOrEmpty(newGame.name))
            {
                return Results.BadRequest("Name is inncorect");
            }
            Game game = new()
            {
                name = newGame.name,
                typeId = newGame.typeId,
                price = newGame.price,
                releaseDate = newGame.releaseDate 
            };
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync(); // async tworzy nowy watek wiec trzeba zatrzymac az zapisze poprawnie

            // dodaje i zwracam w formacie DTO aby zachować zgodność z frontendem 
            GameDetailsDTO gameDto = new ( game.id, game.name, game.typeId, game.price,game.releaseDate);
            // chcemy zwrócic ze poprawnie powstała gra czyli 200 OK , i zawartość jaka została stworzona
            return Results.CreatedAtRoute(GetGameEndpoint, new {id = gameDto.id}, gameDto); // tworzenie ścieżki do odwołania z getem
        });


        groupGames.MapPut( "/{id}", async (int id, UpdateGameDTO updatedGame, GameStoreContext dbContext) =>
        {           
            var existingGame = await dbContext.Games.FindAsync(id);

            if(existingGame is null)
            {
                return Results.NotFound();
            }
            existingGame.name =updatedGame.name;
            existingGame.typeId = updatedGame.typeId;
            existingGame.price = updatedGame.price;
            existingGame.releaseDate = updatedGame.releaseDate;

            await dbContext.SaveChangesAsync();
            // dla wielowoątkowego tu trzeba uwzględnic flagi
            // games[index] = new GameSummaryDTO
            // (
            //     id,
            //     updatedGame.name,
            //     updatedGame.type,
            //     updatedGame.price,
            //     updatedGame.releaseDate
            // );


            return Results.NoContent();
        });

        groupGames.MapDelete( "/{id}", async (int id, GameStoreContext dbContext) =>
        {   
            await dbContext.Games.Where(game => game.id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });
    }


}
