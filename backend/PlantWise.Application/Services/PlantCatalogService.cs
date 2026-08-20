using PlantWise.Application.DTOs;
using PlantWise.Application.Interfaces.Persistence;
using PlantWise.Application.Interfaces.Services;
using PlantWise.Domain.Entities;

namespace PlantWise.Application.Services;

public class PlantCatalogService : IPlantCatalogService
{
    private readonly IPlantCatalogRepository _repository;

    public PlantCatalogService(IPlantCatalogRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PlantCatalogDto>> GetAllAsync()
    {
        var plants = await _repository.GetAllAsync();

        return plants.Select(MapToDto).ToList();
    }

    public async Task<PlantCatalogDto?> GetByIdAsync(int id)
    {
        var plant = await _repository.GetByIdAsync(id);

        return plant == null ? null : MapToDto(plant);
    }

    public async Task<List<PlantCatalogDto>> SearchAsync(string searchTerm)
    {
        var plants = string.IsNullOrWhiteSpace(searchTerm)
            ? await _repository.GetAllAsync()
            : await _repository.SearchAsync(searchTerm);

        return plants.Select(MapToDto).ToList();
    }

    private static PlantCatalogDto MapToDto(PlantCatalog plant)
{
    return new PlantCatalogDto
    {
        Id = plant.Id,
        Name = plant.Name,
        ScientificName = plant.ScientificName,
        Description = plant.Description,
        Difficulty = plant.Difficulty,
        MinimumSpace = plant.MinimumSpace,

        Environments = plant.Environments
            .Select(e => new EnvironmentDto
            {
                Id = e.EnvironmentType.Id,
                Name = e.EnvironmentType.EnvironmentName
            })
            .ToList(),

        Seasons = plant.Seasons
            .Select(s => new SeasonDto
            {
                Id = s.Season.Id,
                Name = s.Season.SeasonName
            })
            .ToList(),

        CareGuide = plant.CareGuide == null
            ? null
            : new PlantCareGuideDto
            {
                Planting = plant.CareGuide.Planting,
                Watering = plant.CareGuide.Watering,
                Light = plant.CareGuide.Light,
                Soil = plant.CareGuide.Soil,
                Fertilizing = plant.CareGuide.Fertilizing,
                Maintenance = plant.CareGuide.Maintenance,
                CommonProblems = plant.CareGuide.CommonProblems
            }
    };
}}