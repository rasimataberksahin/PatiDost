public class AnimalListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Breed { get; set; } = null!;
    public int Age { get; set; }
    public string OwnerName { get; set; } = "-";
}