using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTO
{
    public record CreatGameDTO
    (
        [Required][StringLength(50)]string Name,
        [Required][StringLength(50)]string Genre,
        [Required][StringLength(50)]decimal Price,
        [Required]DateOnly ReleaseDate
    );
}