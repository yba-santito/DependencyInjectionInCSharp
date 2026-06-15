namespace GameStore.Api.DTO
{
//DTO is a contract between the clinet and server since it represesnts 
// a shred agreement about how data will be transferred and used
    public record GameDTO(
        int Id,
        string Name,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate
    );
}