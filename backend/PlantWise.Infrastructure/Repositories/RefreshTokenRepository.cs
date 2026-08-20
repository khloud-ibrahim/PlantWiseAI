using Microsoft.EntityFrameworkCore;
using PlantWise.Application.Interfaces.Persistence;
using PlantWise.Domain.Entities;
using PlantWise.Infrastructure.Persistence.DbContexts;

namespace PlantWise.Infrastructure.Repositories;

public class RefreshTokenRepository
    : GenericRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(PlantWiseDbContext context) : base(context)
    {
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token) =>
        await DbSet.FirstOrDefaultAsync(rt =>
            rt.Token == token && !rt.IsDeleted);
}