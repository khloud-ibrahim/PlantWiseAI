using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PlantWise.Infrastructure.Persistence.DbContexts;

public class PlantWiseDbContextFactory : IDesignTimeDbContextFactory<PlantWiseDbContext>
{
    public PlantWiseDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PlantWiseDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=Khloud\\SQLEXPRESS;Database=PlantWiseDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");

        return new PlantWiseDbContext(optionsBuilder.Options);
    }
}