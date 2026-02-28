namespace App.Api.DTOs
{
    public class AppointmentCreateDto
    {
        public int AnimalId { get; set; }
        public int VeterinarianId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; } = null!;
    }
}
