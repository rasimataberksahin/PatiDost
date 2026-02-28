namespace App.Api.DTOs
{
    public class VeterinarianListDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Specialization { get; set; } = null!;
    }
}
