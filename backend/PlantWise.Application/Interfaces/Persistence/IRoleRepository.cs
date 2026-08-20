using PlantWise.Domain.Entities;

namespace PlantWise.Application.Interfaces.Persistence;

public interface IRoleRepository : IGenericRepository<Role>
{
    Task<Role?> GetByNameAsync(string roleName);
}