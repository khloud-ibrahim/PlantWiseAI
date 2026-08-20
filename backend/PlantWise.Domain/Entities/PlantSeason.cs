namespace PlantWise.Domain.Entities;

public class PlantSeason
{
    public int PlantId { get; set; }

    public Plant Plant { get; set; } = null!;

    public int SeasonId { get; set; }

    public Season Season { get; set; } = null!;
}