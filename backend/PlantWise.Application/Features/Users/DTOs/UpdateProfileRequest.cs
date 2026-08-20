namespace PlantWise.Application.Features.Users.DTOs;

public class UpdateProfileRequest
{
    public string FullName { get; set; } = string.Empty;

    public string ExperienceLevel { get; set; } = string.Empty;
}