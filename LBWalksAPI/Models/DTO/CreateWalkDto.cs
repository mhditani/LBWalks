using System.ComponentModel.DataAnnotations;

namespace LBWalksAPI.Models.DTO
{
    public class CreateWalkDto
    {
        [Required, MaxLength(100, ErrorMessage = "Name has a maximum of 100 characters")]
        public string Name { get; set; } = "";
        [Required]

        public string Description { get; set; } = "";

        [Required]

        public double LengthInKm { get; set; }


        public string? WalkImageUrl { get; set; }

        [Required]

        public Guid DifficultyId { get; set; }
        [Required]

        public Guid RegionId { get; set; }
    }
}
