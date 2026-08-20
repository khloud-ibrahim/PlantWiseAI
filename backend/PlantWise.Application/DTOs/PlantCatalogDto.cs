namespace PlantWise.Application.DTOs;

public class PlantCatalogDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ScientificName { get; set; }
    public string? Description { get; set; }
    public string? Difficulty { get; set; }
    public decimal? MinimumSpace { get; set; }

    public List<EnvironmentDto> Environments { get; set; } = new();
    public List<SeasonDto> Seasons { get; set; } = new();
    public PlantCareGuideDto? CareGuide { get; set; }
}

public class EnvironmentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class SeasonDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
