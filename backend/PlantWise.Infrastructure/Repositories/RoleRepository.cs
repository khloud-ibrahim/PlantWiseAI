using Microsoft.EntityFrameworkCore;
using PlantWise.Application.Interfaces.Persistence;
using PlantWise.Domain.Entities;
using PlantWise.Infrastructure.Persistence.DbContexts;

namespace PlantWise.Infrastructure.Repositories;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(PlantWiseDbContext context) : base(context)
    {
    }

    public async Task<Role?> GetByNameAsync(string roleName) =>
        await DbSet.FirstOrDefaultAsync(r =>
            r.RoleName == roleName && !r.IsDeleted);
}