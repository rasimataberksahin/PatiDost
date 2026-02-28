namespace App.Api.DTOs
{
    public class AnimalUpdateDto
    {
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Breed { get; set; } = null!;
        public int Age { get; set; }
        public int OwnerId { get; set; }
    }
}
