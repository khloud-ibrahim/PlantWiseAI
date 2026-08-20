using Microsoft.EntityFrameworkCore;
using PlantWise.Application.Features.Recommendations.Interfaces;
using PlantWise.Domain.Entities;
using PlantWise.Infrastructure.Persistence.DbContexts;

namespace PlantWise.Infrastructure.Repositories;

public class RecommendationRepository : IRecommendationRepository
{
    private readonly PlantWiseDbContext _context;

    public RecommendationRepository(PlantWiseDbContext context)
    {
        _context = context;
    }

    public async Task<List<PlantCatalog>> GetMatchingPlantsAsync(
        int environmentId,
        int seasonId,
        decimal availableSpace,
        string experienceLevel)
    {
        return await _context.PlantCatalogs
            .Include(p => p.Environments)
            .Include(p => p.Seasons)
            .Where(p =>
                p.MinimumSpace == null ||
                p.MinimumSpace <= availableSpace)
            .Where(p =>
                p.Environments.Any(e =>
                    e.EnvironmentTypeId == environmentId))
            .Where(p =>
                p.Seasons.Any(s =>
                    s.SeasonId == seasonId))
            .Where(p =>
                string.IsNullOrEmpty(experienceLevel) ||
                p.Difficulty == experienceLevel)
            .ToListAsync();
    }
}