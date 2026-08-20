using PlantWise.Application.Features.Users.DTOs;

namespace PlantWise.Application.Features.Users.Interfaces;

public interface IUserService
{
    Task UpdateProfileAsync(
        int userId,
        UpdateProfileRequest request);
}