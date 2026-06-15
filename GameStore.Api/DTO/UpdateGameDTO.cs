namespace GameStore.Api.DTO
{
    public record UpdateGameDTO(
        string Name,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate
    );
}