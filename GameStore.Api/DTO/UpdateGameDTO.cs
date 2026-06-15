using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTO
{
    public record UpdateGameDTO(
        [Required] [StringLength(50)] string Name,
        [StringLength(50)] string Genre,
        [Range(1, 1000)] decimal Price,
        DateOnly ReleaseDate
    );
}
