using PlantWise.Application.DTOs;

namespace PlantWise.Application.Interfaces.Services;

public interface IPlantCatalogService
{
    Task<List<PlantCatalogDto>> GetAllAsync();

    Task<PlantCatalogDto?> GetByIdAsync(int id);

    Task<List<PlantCatalogDto>> SearchAsync(string searchTerm);
}