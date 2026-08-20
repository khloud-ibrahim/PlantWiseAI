namespace PlantWise.Domain.Entities;

public class PlantCareGuide : BaseEntity
{
    public int PlantCatalogId { get; set; }

    public PlantCatalog PlantCatalog { get; set; } = null!;

    public string Planting { get; set; } = string.Empty;

    public string Watering { get; set; } = string.Empty;

    public string Light { get; set; } = string.Empty;

    public string Soil { get; set; } = string.Empty;

    public string Fertilizing { get; set; } = string.Empty;

    public string Maintenance { get; set; } = string.Empty;

    public string CommonProblems { get; set; } = string.Empty;
}