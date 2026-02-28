using System.ComponentModel.DataAnnotations;

namespace App.Api.DTOs
{
    public class VeterinarianCreateDto
    {
        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string Specialization { get; set; } = null!;
    }
}
