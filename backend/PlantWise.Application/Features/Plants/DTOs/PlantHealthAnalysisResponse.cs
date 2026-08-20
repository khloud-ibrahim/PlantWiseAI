namespace PlantWise.Application.Features.Plants.DTOs;

public class PlantHealthAnalysisResponse
{
    public string PlantName { get; set; } = string.Empty;

    public string HealthStatus { get; set; } = string.Empty;

    public string DiseaseOrProblem { get; set; } = string.Empty;

    public double Confidence { get; set; }

    public List<string> Symptoms { get; set; } = new();

    public List<string> Treatment { get; set; } = new();

    public List<string> CareTips { get; set; } = new();
}