using PlantWise.Application.Features.Recommendations.DTOs;

namespace PlantWise.Application.Features.Recommendations.Interfaces;

public interface IRecommendationService
{
    Task<List<RecommendationResponseDto>> GetRecommendationsAsync(
        RecommendationRequestDto request,
        int userId);
}