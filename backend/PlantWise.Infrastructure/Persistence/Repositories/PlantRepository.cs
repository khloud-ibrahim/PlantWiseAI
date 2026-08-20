using Microsoft.EntityFrameworkCore;
using PlantWise.Application.Interfaces.Persistence;
using PlantWise.Domain.Entities;
using PlantWise.Infrastructure.Persistence.DbContexts;

namespace PlantWise.Infrastructure.Persistence.Repositories;

public class PlantRepository : IPlantRepository
{
    private readonly PlantWiseDbContext _context;

    public PlantRepository(PlantWiseDbContext context)
    {
        _context = context;
    }

    public async Task<Plant?> GetByIdAsync(int id)
    {
        return await _context.Plants
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Plant>> GetByUserIdAsync(int userId)
    {
        return await _context.Plants
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.Id)
            .ToListAsync();
    }

    public async Task AddAsync(Plant plant)
    {
        await _context.Plants.AddAsync(plant);
    }

    public void Update(Plant plant)
    {
        _context.Plants.Update(plant);
    }

    public void Delete(Plant plant)
    {
        _context.Plants.Remove(plant);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}