namespace PlantWise.Application.Features.Recommendations.DTOs;

public class RecommendationRequestDto
{
    public int EnvironmentId { get; set; }

    public decimal AvailableSpace { get; set; }

    public int SeasonId { get; set; }

    public string ExperienceLevel { get; set; } = string.Empty;
}