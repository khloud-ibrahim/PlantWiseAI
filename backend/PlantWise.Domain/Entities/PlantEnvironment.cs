namespace PlantWise.Domain.Entities;

public class PlantEnvironment
{
    public int PlantId { get; set; }

    public Plant Plant { get; set; } = null!;

    public int EnvironmentId { get; set; }

    public EnvironmentType EnvironmentType { get; set; } = null!;
}