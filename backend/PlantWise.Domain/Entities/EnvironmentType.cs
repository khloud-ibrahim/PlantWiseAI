namespace PlantWise.Domain.Entities;

public class EnvironmentType : BaseEntity
{
    public string EnvironmentName { get; set; } = string.Empty;

    public ICollection<PlantEnvironment> PlantEnvironments { get; set; }
        = new List<PlantEnvironment>();
}