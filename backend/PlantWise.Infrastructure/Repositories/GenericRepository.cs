using Microsoft.EntityFrameworkCore;
using PlantWise.Application.Interfaces.Persistence;
using PlantWise.Domain.Entities;
using PlantWise.Infrastructure.Persistence.DbContexts;

namespace PlantWise.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly PlantWiseDbContext Context;
    protected readonly DbSet<T> DbSet;

    public GenericRepository(PlantWiseDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id) =>
        await DbSet.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

    public async Task<IReadOnlyList<T>> GetAllAsync() =>
        await DbSet.Where(x => !x.IsDeleted).ToListAsync();

    public async Task AddAsync(T entity) =>
        await DbSet.AddAsync(entity);
    public async Task SaveChangesAsync()
{
    await Context.SaveChangesAsync();
}    

    public void Update(T entity) =>
        DbSet.Update(entity);

    public void Delete(T entity)
    {
        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        DbSet.Update(entity);
    }
}