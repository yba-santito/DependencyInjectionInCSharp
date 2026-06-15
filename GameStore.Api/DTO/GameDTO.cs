using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTO
{
    //DTO is a contract between the clinet and server since it represesnts
    // a shred agreement about how data will be transferred and used
    public record GameDTO(
        int Id,
        [Required] [StringLength(50)] string Name,
        [StringLength(50)] string Genre,
        [Range(1, 1000)] decimal Price,
        DateOnly ReleaseDate
    );
}
