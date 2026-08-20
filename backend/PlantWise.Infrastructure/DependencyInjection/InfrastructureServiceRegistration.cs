using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlantWise.Application.Interfaces.Persistence;
using PlantWise.Application.Interfaces.Services;
using PlantWise.Infrastructure.Persistence.DbContexts;
using PlantWise.Infrastructure.Repositories;
using PlantWise.Infrastructure.Services;
using PlantWise.Shared.Settings;
using Microsoft.Extensions.Options;
using PlantWise.Application.Features.Plants.Interfaces;
using PlantWise.Infrastructure.AI;

using PlantWise.Infrastructure.Persistence.Repositories;
using PlantWise.Application.Features.Recommendations.Interfaces;


namespace PlantWise.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<JwtSettings>()
    .Bind(configuration.GetSection("JwtSettings"));

        services.Configure<OpenAISettings>(
                 configuration.GetSection("OpenAI"));
                 
            services.AddDbContext<PlantWiseDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<IPasswordHasher, PasswordHasherService>();
        services.AddScoped<IJwtService, JwtService>();
services.AddScoped<IPlantRepository, PlantRepository>();
        services.AddScoped<IPlantHealthService, OpenAIPlantHealthService>();
        services.AddScoped<IPlantHealthService, OpenAIPlantHealthService>();
        services.AddScoped<IRecommendationRepository, RecommendationRepository>();
        return services;
        
    }
}