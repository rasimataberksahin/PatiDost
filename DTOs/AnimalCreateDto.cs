using System.ComponentModel.DataAnnotations;

namespace App.Api.DTOs
{
    public class AnimalCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Type { get; set; } = null!;

        [Required]
        public string Breed { get; set; } = null!;

        [Range(0, 50)]
        public int Age { get; set; }

        [Required]
        public int OwnerId { get; set; }
    }
}

