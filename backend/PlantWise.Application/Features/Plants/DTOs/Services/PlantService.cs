using PlantWise.Application.Features.Plants.DTOs;
using PlantWise.Application.Features.Plants.Interfaces;
using PlantWise.Application.Interfaces.Persistence;
using PlantWise.Domain.Entities;

namespace PlantWise.Application.Features.Plants.Services;

public class PlantService : IPlantService
{
    private readonly IPlantRepository _plantRepository;

    public PlantService(IPlantRepository plantRepository)
    {
        _plantRepository = plantRepository;
    }

    public async Task<PlantResponse> CreateAsync(
        int userId,
        CreatePlantRequest request)
    {
        var plant = new Plant
        {
            UserId = userId,
            Name = request.Name,
            Species = request.Species,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            PlantedDate = request.PlantedDate,
            Location = request.Location,
            Notes = request.Notes
        };

        await _plantRepository.AddAsync(plant);
        await _plantRepository.SaveChangesAsync();

        return MapToResponse(plant);
    }

    public async Task<List<PlantResponse>> GetMyPlantsAsync(
        int userId)
    {
        var plants = await _plantRepository.GetByUserIdAsync(userId);

        return plants
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<PlantResponse> GetByIdAsync(
        int userId,
        int plantId)
    {
        var plant = await _plantRepository.GetByIdAsync(plantId);

        if (plant is null || plant.UserId != userId)
            throw new KeyNotFoundException("Plant not found.");

        return MapToResponse(plant);
    }

    public async Task UpdateAsync(
        int userId,
        int plantId,
        CreatePlantRequest request)
    {
        var plant = await _plantRepository.GetByIdAsync(plantId);

        if (plant is null || plant.UserId != userId)
            throw new KeyNotFoundException("Plant not found.");

        plant.Name = request.Name;
        plant.Species = request.Species;
        plant.Description = request.Description;
        plant.ImageUrl = request.ImageUrl;
        plant.PlantedDate = request.PlantedDate;
        plant.Location = request.Location;
        plant.Notes = request.Notes;

        _plantRepository.Update(plant);

        await _plantRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(
        int userId,
        int plantId)
    {
        var plant = await _plantRepository.GetByIdAsync(plantId);

        if (plant is null || plant.UserId != userId)
            throw new KeyNotFoundException("Plant not found.");

        _plantRepository.Delete(plant);

        await _plantRepository.SaveChangesAsync();
    }

    private static PlantResponse MapToResponse(Plant plant)
    {
        return new PlantResponse
        {
            Id = plant.Id,
            Name = plant.Name,
            Species = plant.Species,
            Description = plant.Description,
            ImageUrl = plant.ImageUrl,
            PlantedDate = plant.PlantedDate,
            Location = plant.Location,
            Notes = plant.Notes
        };
    }
}