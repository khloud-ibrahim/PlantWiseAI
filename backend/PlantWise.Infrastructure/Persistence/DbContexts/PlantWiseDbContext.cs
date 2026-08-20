using Microsoft.EntityFrameworkCore;
using PlantWise.Domain.Entities;

namespace PlantWise.Infrastructure.Persistence.DbContexts;

public class PlantWiseDbContext : DbContext
{
    public PlantWiseDbContext(
        DbContextOptions<PlantWiseDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<Plant> Plants => Set<Plant>();
    public DbSet<PlantCategory> PlantCategories => Set<PlantCategory>();
    public DbSet<EnvironmentType> EnvironmentTypes => Set<EnvironmentType>();
    public DbSet<PlantEnvironment> PlantEnvironments => Set<PlantEnvironment>();
    public DbSet<Season> Seasons => Set<Season>();
    public DbSet<PlantSeason> PlantSeasons => Set<PlantSeason>();
    public DbSet<Recommendation> Recommendations => Set<Recommendation>();
public DbSet<PlantCatalog> PlantCatalogs => Set<PlantCatalog>();

public DbSet<PlantCatalogEnvironment> PlantCatalogEnvironments
    => Set<PlantCatalogEnvironment>();

public DbSet<PlantCatalogSeason> PlantCatalogSeasons
    => Set<PlantCatalogSeason>();
    public DbSet<PlantCareGuide> PlantCareGuides
    => Set<PlantCareGuide>();
   protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyConfigurationsFromAssembly(
        typeof(PlantWiseDbContext).Assembly);

    modelBuilder.Entity<PlantCatalogEnvironment>()
        .HasKey(x => new
        {
            x.PlantCatalogId,
            x.EnvironmentId
        });

    modelBuilder.Entity<PlantCatalogSeason>()
        .HasKey(x => new
        {
            x.PlantCatalogId,
            x.SeasonId
        });
        modelBuilder.Entity<PlantEnvironment>()
    .HasKey(x => new
    {
        x.PlantId,
        x.EnvironmentId
    });

modelBuilder.Entity<PlantSeason>()
    .HasKey(x => new
    {
        x.PlantId,
        x.SeasonId
    });
    modelBuilder.Entity<Recommendation>()
    .HasOne(r => r.User)
    .WithMany()
    .HasForeignKey(r => r.UserId)
    .OnDelete(DeleteBehavior.NoAction);
    modelBuilder.Entity<Recommendation>()
    .HasOne(r => r.Plant)
    .WithMany()
    .HasForeignKey(r => r.PlantId)
    .OnDelete(DeleteBehavior.NoAction);
    modelBuilder.Entity<PlantCatalog>()
    .HasOne(p => p.CareGuide)
    .WithOne(c => c.PlantCatalog)
    .HasForeignKey<PlantCareGuide>(c => c.PlantCatalogId)
    .OnDelete(DeleteBehavior.Cascade);
}
}