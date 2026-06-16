using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTO
{
    //DTO is a contract between the clinet and server since it represesnts
    // a shred agreement about how data will be transferred and used
    public record GameDetailsDTO(
        int Id,
        [Required] [StringLength(50)] string Name,
        [Range(1, 50)] int GenreId,
        [Range(1, 1000)] decimal Price,
        DateOnly ReleaseDate
    );
}
