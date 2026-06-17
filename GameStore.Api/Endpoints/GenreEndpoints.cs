using GameStore.Api.Data;
using GameStore.Api.DTO;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints
{
    public static class GenreEndpoints
    {
        const string GetGenreEndpointName = "GetGenre";

        public static void MapGenresEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/genres");
            //Get /games
            group.MapGet(
                "/",
                async (GameStoreContext dbContext) =>
                    await dbContext
                        .Games
                        .Select(genre => new GenreDTO(
                            genre.Id,
                            genre.Name
                        ))
                        .ToListAsync()
            );

            //Get games/id
            group
                .MapGet(
                    "/{id}",
                    async (int id, GameStoreContext dbContext) =>
                    {
                        var genre = await dbContext.Genres.FindAsync(id);
                        return genre is null
                            ? Results.NotFound()
                            : Results.Ok(
                                new GenreDTO(
                                    genre.Id,
                                    genre.Name

                                )
                            );
                    }
                )
                .WithName(GetGenreEndpointName);

            // Create a entry

            group.MapPost(
                "/",
                async (GenreDTO newGenre, GameStoreContext dbContext) =>
                {
                    Genre genre = new()
                    {
                        Name = newGenre.Name,
                    };

                    dbContext.Genres.Add(genre);
                    await dbContext.SaveChangesAsync();
                    // games.Add(game);

                    GenreDTO genreDto = new(
                        genre.Id,
                        genre.Name
                    );

                    return Results.CreatedAtRoute(
                        GetGenreEndpointName,
                        new { id = genreDto.Id },
                        genre
                    );
                }
            );
        }
    }
}
