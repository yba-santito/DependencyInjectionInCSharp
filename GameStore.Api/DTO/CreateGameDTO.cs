namespace GameStore.Api.DTO
{
    public record CreatGameDTO
    (
        string Name,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate
    );
}