namespace PlantWise.Domain.Entities;

public class PlantCatalogSeason
{
    public int PlantCatalogId { get; set; }

    public PlantCatalog PlantCatalog { get; set; } = null!;

    public int SeasonId { get; set; }

    public Season Season { get; set; } = null!;
}