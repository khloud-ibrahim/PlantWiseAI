using PlantWise.Application.Features.Plants.DTOs;

namespace PlantWise.Application.Features.Plants.Interfaces;

public interface IPlantService
{
    Task<PlantResponse> CreateAsync(
        int userId,
        CreatePlantRequest request);

    Task<List<PlantResponse>> GetMyPlantsAsync(
        int userId);

    Task<PlantResponse> GetByIdAsync(
        int userId,
        int plantId);

    Task UpdateAsync(
        int userId,
        int plantId,
        CreatePlantRequest request);

    Task DeleteAsync(
        int userId,
        int plantId);
}