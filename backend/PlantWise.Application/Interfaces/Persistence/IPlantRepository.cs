using PlantWise.Domain.Entities;

namespace PlantWise.Application.Interfaces.Persistence;

public interface IPlantRepository
{
    Task<Plant?> GetByIdAsync(int id);

    Task<List<Plant>> GetByUserIdAsync(int userId);

    Task AddAsync(Plant plant);

    void Update(Plant plant);

    void Delete(Plant plant);

    Task SaveChangesAsync();
}