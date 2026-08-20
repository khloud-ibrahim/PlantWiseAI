namespace PlantWise.Domain.Entities;

public class PlantCategory : BaseEntity
{
    public string CategoryName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public ICollection<Plant> Plants { get; set; } = new List<Plant>();
}