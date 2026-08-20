namespace PlantWise.Domain.Entities;

public class Recommendation : BaseEntity
{
    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public int PlantId { get; set; }

    public Plant Plant { get; set; } = null!;

    public string? Reason { get; set; }
}