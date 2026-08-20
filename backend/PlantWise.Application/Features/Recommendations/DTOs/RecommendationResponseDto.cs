namespace PlantWise.Application.Features.Recommendations.DTOs;

public class RecommendationResponseDto
{
    public int PlantId { get; set; }

    public string PlantName { get; set; } = string.Empty;

    public string? ScientificName { get; set; }

    public string? Description { get; set; }

    public string? Difficulty { get; set; }

    public int? GrowthDuration { get; set; }

    public string Reason { get; set; } = string.Empty;
}