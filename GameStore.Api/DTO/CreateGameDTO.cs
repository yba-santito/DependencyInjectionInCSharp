using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTO
{
    public record CreatGameDTO
    (
        [Required]string Name,
        [Required]string Genre,
        [Required]decimal Price,
        [Required]DateOnly ReleaseDate
    );
}