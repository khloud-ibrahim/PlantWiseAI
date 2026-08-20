using PlantWise.Domain.Entities;

namespace PlantWise.Application.Interfaces.Persistence;

public interface IPlantCatalogRepository
{
    Task<PlantCatalog?> GetByIdAsync(int id);

    Task<List<PlantCatalog>> GetAllAsync();

    Task<List<PlantCatalog>> SearchAsync(string searchTerm);

    Task AddAsync(PlantCatalog plant);

    void Update(PlantCatalog plant);

    void Delete(PlantCatalog plant);

    Task SaveChangesAsync();
}