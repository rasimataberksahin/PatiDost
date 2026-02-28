namespace App.Api.DTOs
{
    public class AppointmentListDto
    {
        public int Id { get; set; }
        public string AnimalName { get; set; } = null!;
        public string VeterinarianName { get; set; } = null!;
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; } = null!;
    }
}
