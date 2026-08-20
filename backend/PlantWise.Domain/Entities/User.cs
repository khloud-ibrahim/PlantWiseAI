namespace PlantWise.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string? ProfileImage { get; set; }

    public string? ExperienceLevel { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
        = new List<UserRole>();

    public ICollection<RefreshToken> RefreshTokens { get; set; }
        = new List<RefreshToken>();
}