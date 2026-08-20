namespace PlantWise.Domain.Entities;

public class Plant : BaseEntity
{
    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string? Species { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? PlantedDate { get; set; }

    public string? Location { get; set; }

    public string? Notes { get; set; }}
