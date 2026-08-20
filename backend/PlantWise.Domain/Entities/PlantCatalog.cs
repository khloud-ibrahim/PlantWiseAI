namespace PlantWise.Domain.Entities;

public class PlantCatalog : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string? ScientificName { get; set; }

    public string? Description { get; set; }

    public string? Difficulty { get; set; }

    public decimal? MinimumSpace { get; set; }

    public ICollection<PlantCatalogEnvironment> Environments { get; set; }
        = new List<PlantCatalogEnvironment>();

    public ICollection<PlantCatalogSeason> Seasons { get; set; }
        = new List<PlantCatalogSeason>();

        public PlantCareGuide? CareGuide { get; set; }
}