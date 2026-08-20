using PlantWise.Application.Features.Users.DTOs;
using PlantWise.Application.Features.Users.Interfaces;
using PlantWise.Application.Interfaces.Persistence;

namespace PlantWise.Application.Features.Users.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task UpdateProfileAsync(
        int userId,
        UpdateProfileRequest request)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
            throw new KeyNotFoundException("User not found.");

        user.FullName = request.FullName;
        user.ExperienceLevel = request.ExperienceLevel;

        await _userRepository.SaveChangesAsync();
    }
}