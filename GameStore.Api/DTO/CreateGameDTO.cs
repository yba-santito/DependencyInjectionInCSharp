using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTO
{
    public record CreatGameDTO
    (
        [Required][StringLength(50)]string Name,
        [Required][StringLength(50)]string Genre,
        [Required][Range(1,1000)]decimal Price,
        [Required]DateOnly ReleaseDate
    );
}