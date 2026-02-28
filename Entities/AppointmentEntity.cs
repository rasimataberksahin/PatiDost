namespace App.Api.Entities
{
    public class AppointmentEntity
    {
        public int Id { get; set; }

        public int AnimalId { get; set; }
        public AnimalEntity Animal { get; set; } = null!;

        public int VeterinarianId { get; set; }
        public VeterinarianEntity Veterinarian { get; set; } = null!;

        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; } = null!;
    }
}
