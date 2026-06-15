using GameStore.Api.DTO;


const string GetGameEndpointName = "GetGame";

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


List<GameDTO> games = [
    new (1, "StreetFighter 2", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
    new (2, "Final Fantasy VII Rebirth", "RPG", 69.99M, new DateOnly(2024, 2, 29)),
    new (3, "Astro Bot", "Platformer", 59.99M, new DateOnly(2024, 9, 6))
];
//Get /games
app.MapGet("/games", () => games);


//Get games/id
app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id)).
WithName(GetGameEndpointName);

// Create a entry

app.MapPost("/games", (CreatGameDTO newGame)=>
{
    GameDTO game = new (
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new{id =game.Id}, game);
});

//PUT /games/id

app.MapPut("/games/{id}", (int id, UpdateGameDTO updatedGame) =>
{
    var index = games.FindIndex(games => games.Id == id);

    games[index] = new GameDTO(id,
    updatedGame.Name,
    updatedGame.Genre,
    updatedGame.Price,
    updatedGame.ReleaseDate);

    return Results.NoContent();
});

//Delete /games/id

app.MapDelete("/games/{id}", (int id) =>
{
    games.RemoveAll(games => games.Id == id);

    return Results.NoContent();
});
app.Run();
