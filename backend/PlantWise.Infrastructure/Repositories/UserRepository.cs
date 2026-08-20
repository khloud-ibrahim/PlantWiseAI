using Microsoft.EntityFrameworkCore;
using PlantWise.Application.Interfaces.Persistence;
using PlantWise.Domain.Entities;
using PlantWise.Infrastructure.Persistence.DbContexts;

namespace PlantWise.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(PlantWiseDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email) =>
        await DbSet
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u =>
                u.Email == email && !u.IsDeleted);
}