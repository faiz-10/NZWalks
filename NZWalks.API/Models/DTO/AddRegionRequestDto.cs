using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code cant be less than 3 characters")]
        [MaxLength(3, ErrorMessage = "Code cant be more than 3 characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name cant have more than 100 characters")]
        public string Name { get; set; }

        public string? RegionImageURL { get; set; }
    }
}
