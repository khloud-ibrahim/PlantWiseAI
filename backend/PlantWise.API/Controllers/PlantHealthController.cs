using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlantWise.Application.Features.Plants.Interfaces;

namespace PlantWise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PlantHealthController : ControllerBase
{
    private readonly IPlantHealthService _plantHealthService;

    public PlantHealthController(
        IPlantHealthService plantHealthService)
    {
        _plantHealthService = plantHealthService;
    }

    [HttpPost("analyze")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Analyze(
        IFormFile image,
        [FromForm] string? plantName)
    {
        if (image is null || image.Length == 0)
        {
            return BadRequest(new
            {
                message = "Please upload a plant image."
            });
        }

        if (!image.ContentType.StartsWith("image/"))
        {
            return BadRequest(new
            {
                message = "Only image files are allowed."
            });
        }

        await using var stream = image.OpenReadStream();

        var result = await _plantHealthService.AnalyzeAsync(
            stream,
            image.FileName,
            image.ContentType,
            plantName);

        return Ok(result);
    }
}