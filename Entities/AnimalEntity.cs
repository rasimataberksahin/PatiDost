namespace App.Api.Entities
{
    public class AnimalEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Breed { get; set; } = null!;
        public int Age { get; set; }

        public int OwnerId { get; set; }
        public OwnerEntity Owner { get; set; } = null!;
    }
}
