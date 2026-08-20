using PlantWise.Domain.Entities;

namespace PlantWise.Application.Interfaces.Services;

public interface IJwtService
{
    string GenerateToken(User user);
}