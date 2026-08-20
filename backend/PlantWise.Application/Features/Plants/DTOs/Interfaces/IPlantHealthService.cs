using PlantWise.Application.Features.Plants.DTOs;

namespace PlantWise.Application.Features.Plants.Interfaces;

public interface IPlantHealthService
{
    Task<PlantHealthAnalysisResponse> AnalyzeAsync(
        Stream imageStream,
        string fileName,
        string contentType,
        string? plantName = null);
}