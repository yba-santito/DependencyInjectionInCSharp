namespace GameStore.Api.DTO
{
    public record DeletedGameDTO(
        int Id,
        string Name,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate
    );
}