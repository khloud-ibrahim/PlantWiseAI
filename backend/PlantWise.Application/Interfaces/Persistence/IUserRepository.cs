using PlantWise.Domain.Entities;

namespace PlantWise.Application.Interfaces.Persistence;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}