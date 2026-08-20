namespace PlantWise.Domain.Entities;

public class PlantCatalogEnvironment
{
    public int PlantCatalogId { get; set; }

    public PlantCatalog PlantCatalog { get; set; } = null!;

    public int EnvironmentId { get; set; }

    public int EnvironmentTypeId { get; set; }

    public EnvironmentType EnvironmentType { get; set; } = null!;
}