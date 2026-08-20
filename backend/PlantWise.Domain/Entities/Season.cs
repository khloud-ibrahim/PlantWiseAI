namespace PlantWise.Domain.Entities;

public class Season : BaseEntity
{
    public string SeasonName { get; set; } = string.Empty;

    public ICollection<PlantSeason> PlantSeasons { get; set; }
        = new List<PlantSeason>();
}