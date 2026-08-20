using Microsoft.Extensions.DependencyInjection;
using PlantWise.Application.Features.Authentication.Services;
using PlantWise.Application.Features.Users.Interfaces;
using PlantWise.Application.Features.Users.Services;
using PlantWise.Application.Features.Plants.Interfaces;
using PlantWise.Application.Features.Plants.Services;
using PlantWise.Application.Features.Recommendations.Interfaces;
using PlantWise.Application.Features.Recommendations.Services;

namespace PlantWise.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPlantService, PlantService>();
        services.AddScoped<IRecommendationService, RecommendationService>();
        return services;
    }
}