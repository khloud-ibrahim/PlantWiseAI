using PlantWise.Domain.Entities;

namespace PlantWise.Application.Features.Recommendations.Interfaces;

public interface IRecommendationRepository
{
    Task<List<PlantCatalog>> GetMatchingPlantsAsync(
        int environmentId,
        int seasonId,
        decimal availableSpace,
        string experienceLevel);
}