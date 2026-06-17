using GameStore.Api.Data;
using GameStore.Api.DTO;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints
{
    public static class GamesEndpoints
    {
        const string GetGameEndpointName = "GetGame";

        private static readonly List<GameSummaryDTO> games =
        [
            new(1, "StreetFighter 2", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
            new(2, "Final Fantasy VII Rebirth", "RPG", 69.99M, new DateOnly(2024, 2, 29)),
            new(3, "Astro Bot", "Platformer", 59.99M, new DateOnly(2024, 9, 6)),
        ];

        public static void MapGamesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/games");
            //Get /games
            group.MapGet(
                "/",
                async (GameStoreContext dbContext) =>
                    await dbContext
                        .Games.Include(game => game.Genre)
                        .Select(game => new GameSummaryDTO(
                            game.Id,
                            game.Name,
                            game.Genre!.Name,
                            game.Price,
                            game.ReleaseDate
                        ))
                        .ToListAsync()
            );

            //Get games/id
            group
                .MapGet(
                    "/{id}",
                    async (int id, GameStoreContext dbContext) =>
                    {
                        var game = await dbContext.Games.FindAsync(id);
                        return game is null
                            ? Results.NotFound()
                            : Results.Ok(
                                new GameDetailsDTO(
                                    game.Id,
                                    game.Name,
                                    game.GenreId,
                                    game.Price,
                                    game.ReleaseDate
                                )
                            );
                    }
                )
                .WithName(GetGameEndpointName);

            // Create a entry

            group.MapPost(
                "/",
                async (CreatGameDTO newGame, GameStoreContext dbContext) =>
                {
                    Game game = new()
                    {
                        Name = newGame.Name,
                        GenreId = newGame.GenreId,
                        Price = newGame.Price,
                        ReleaseDate = newGame.ReleaseDate,
                    };

                    dbContext.Games.Add(game);
                    await dbContext.SaveChangesAsync();
                    // games.Add(game);

                    GameDetailsDTO gameDTO = new(
                        game.Id,
                        game.Name,
                        game.GenreId,
                        game.Price,
                        game.ReleaseDate
                    );

                    return Results.CreatedAtRoute(
                        GetGameEndpointName,
                        new { id = gameDTO.Id },
                        game
                    );
                }
            );

            //PUT /games/id

            group.MapPut(
                "/{id}",
                async (int id, UpdateGameDTO updatedGame, GameStoreContext dbContext) =>
                {
                    var seekingGame = await dbContext.Games.FindAsync(id);
                    if (seekingGame == null)
                    {
                        return Results.NotFound();
                    }

                    seekingGame.Name = updatedGame.Name;
                    seekingGame.GenreId = updatedGame.GenreId;
                    seekingGame.Price = updatedGame.Price;
                    seekingGame.ReleaseDate = updatedGame.ReleaseDate;

                    await dbContext.SaveChangesAsync();
                    return Results.NoContent();
                }
            );

            //Delete /games/id

            group.MapDelete(
                "/{id}",
                (int id) =>
                {
                    games.RemoveAll(games => games.Id == id);

                    return Results.NoContent();
                }
            );
        }
    }
}
