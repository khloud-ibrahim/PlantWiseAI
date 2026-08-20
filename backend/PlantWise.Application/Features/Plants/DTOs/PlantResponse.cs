namespace PlantWise.Application.Features.Plants.DTOs;

public class PlantResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Species { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? PlantedDate { get; set; }

    public string? Location { get; set; }

    public string? Notes { get; set; }
}