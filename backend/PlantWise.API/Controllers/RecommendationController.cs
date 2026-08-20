using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlantWise.Application.Features.Recommendations.DTOs;
using PlantWise.Application.Features.Recommendations.Interfaces;
using System.Security.Claims;

namespace PlantWise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RecommendationController : ControllerBase
{
    private readonly IRecommendationService _service;

    public RecommendationController(IRecommendationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> GetRecommendations(
        [FromBody] RecommendationRequestDto request)
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst("sub")?.Value;

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var result = await _service.GetRecommendationsAsync(
            request,
            userId);

        return Ok(result);
    }
}