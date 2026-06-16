using GameStore.Api.Data;
using GameStore.Api.DTO;
using GameStore.Api.Models;

namespace GameStore.Api.Endpoints
{
    public static class GamesEndpoints
    {
        const string GetGameEndpointName = "GetGame";

        private static readonly List<GameDTO> games =
        [
            new(1, "StreetFighter 2", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
            new(2, "Final Fantasy VII Rebirth", "RPG", 69.99M, new DateOnly(2024, 2, 29)),
            new(3, "Astro Bot", "Platformer", 59.99M, new DateOnly(2024, 9, 6)),
        ];

        public static void MapGamesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/game");
            //Get /games
            group.MapGet("/", () => games);

            //Get games/id
            group
                .MapGet(
                    "/{id}",
                    (int id) =>
                    {
                        var game = games.Find(game => game.Id == id);
                        return game is null ? Results.NotFound() : Results.Ok(game);
                    }
                )
                .WithName(GetGameEndpointName);

            // Create a entry

            group.MapPost(
                "/",
                (CreatGameDTO newGame, GameStoreContext dbContext) =>
                {
                    Game game = new()
                    {
                        Id = games.Max(g => g.Id) + 1,
                        Name = newGame.Name,
                        GenreId = newGame.GenreId,
                        Price = newGame.Price,
                        ReleaseDate = newGame.ReleaseDate,
                    };

                    dbContext.Games.Add(game);
                    dbContext.SaveChanges();
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
                        gameDTO
                    );
                }
            );

            //PUT /games/id

            group.MapPut(
                "/{id}",
                (int id, UpdateGameDTO updatedGame) =>
                {
                    var index = games.FindIndex(games => games.Id == id);
                    if (index == -1)
                    {
                        return Results.NotFound();
                    }
                    games[index] = new GameDTO(
                        id,
                        updatedGame.Name,
                        updatedGame.Genre,
                        updatedGame.Price,
                        updatedGame.ReleaseDate
                    );

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
