using Microsoft.EntityFrameworkCore;
using PlantWise.Application.Interfaces.Persistence;
using PlantWise.Domain.Entities;
using PlantWise.Infrastructure.Persistence.DbContexts;

namespace PlantWise.Infrastructure.Persistence.Repositories;

public class PlantCatalogRepository : IPlantCatalogRepository
{
    private readonly PlantWiseDbContext _context;

    public PlantCatalogRepository(PlantWiseDbContext context)
    {
        _context = context;
    }
public async Task<PlantCatalog?> GetByIdAsync(int id)
{
    return await _context.PlantCatalogs
        .Include(p => p.Environments)
            .ThenInclude(e => e.EnvironmentType)
        .Include(p => p.Seasons)
            .ThenInclude(s => s.Season)
        .Include(p => p.CareGuide)
        .FirstOrDefaultAsync(p => p.Id == id);
}

  public async Task<List<PlantCatalog>> GetAllAsync()
{
    return await _context.PlantCatalogs
        .Include(p => p.Environments)
            .ThenInclude(e => e.EnvironmentType)
        .Include(p => p.Seasons)
            .ThenInclude(s => s.Season)
        .Include(p => p.CareGuide)
        .OrderBy(p => p.Name)
        .ToListAsync();
}
public async Task<List<PlantCatalog>> SearchAsync(string searchTerm)
{
    return await _context.PlantCatalogs
        .Include(p => p.Environments)
            .ThenInclude(e => e.EnvironmentType)
        .Include(p => p.Seasons)
            .ThenInclude(s => s.Season)
        .Include(p => p.CareGuide)
        .Where(p =>
            p.Name.Contains(searchTerm) ||
            (p.ScientificName != null &&
             p.ScientificName.Contains(searchTerm)))
        .OrderBy(p => p.Name)
        .ToListAsync();
}

    public async Task AddAsync(PlantCatalog plant)
    {
        await _context.PlantCatalogs.AddAsync(plant);
    }

    public void Update(PlantCatalog plant)
    {
        _context.PlantCatalogs.Update(plant);
    }

    public void Delete(PlantCatalog plant)
    {
        _context.PlantCatalogs.Remove(plant);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}