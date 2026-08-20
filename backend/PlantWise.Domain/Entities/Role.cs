namespace PlantWise.Domain.Entities;

public class Role : BaseEntity
{
    public string RoleName { get; set; } = string.Empty;

    public ICollection<UserRole> UserRoles { get; set; }
        = new List<UserRole>();
}