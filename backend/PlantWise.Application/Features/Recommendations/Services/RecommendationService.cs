using PlantWise.Application.Features.Recommendations.DTOs;
using PlantWise.Application.Features.Recommendations.Interfaces;

namespace PlantWise.Application.Features.Recommendations.Services;

public class RecommendationService : IRecommendationService
{
    private readonly IRecommendationRepository _repository;

    public RecommendationService(IRecommendationRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<RecommendationResponseDto>> GetRecommendationsAsync(
        RecommendationRequestDto request,
        int userId)
    {
       var plants = await _repository.GetMatchingPlantsAsync(
    request.EnvironmentId,
    request.SeasonId,
    request.AvailableSpace,
    request.ExperienceLevel);

        return plants.Select(p => new RecommendationResponseDto
        {
            PlantId = p.Id,
            PlantName = p.Name,
            ScientificName = p.ScientificName,
            Description = p.Description,
            Difficulty = p.Difficulty,

            Reason =
                $"Suitable for your selected environment, season, available space, and experience level."
        }).ToList();
    }
}