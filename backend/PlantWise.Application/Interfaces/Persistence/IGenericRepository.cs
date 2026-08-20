using PlantWise.Domain.Entities;

namespace PlantWise.Application.Interfaces.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task SaveChangesAsync();
    void Update(T entity);
    void Delete(T entity);
}