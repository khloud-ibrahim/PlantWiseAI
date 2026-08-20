using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlantWise.Application.Features.Plants.DTOs;
using PlantWise.Application.Features.Plants.Interfaces;

namespace PlantWise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PlantsController : ControllerBase
{
    private readonly IPlantService _plantService;

    public PlantsController(IPlantService plantService)
    {
        _plantService = plantService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreatePlantRequest request)
    {
        var userId = GetUserId();

        var plant = await _plantService.CreateAsync(userId, request);

        return Ok(plant);
    }

    [HttpGet]
    public async Task<IActionResult> GetMyPlants()
    {
        var userId = GetUserId();

        var plants = await _plantService.GetMyPlantsAsync(userId);

        return Ok(plants);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = GetUserId();

        try
        {
            var plant = await _plantService.GetByIdAsync(userId, id);

            return Ok(plant);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new
            {
                message = "Plant not found."
            });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] CreatePlantRequest request)
    {
        var userId = GetUserId();

        try
        {
            await _plantService.UpdateAsync(userId, id, request);

            return Ok(new
            {
                message = "Plant updated successfully."
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new
            {
                message = "Plant not found."
            });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetUserId();

        try
        {
            await _plantService.DeleteAsync(userId, id);

            return Ok(new
            {
                message = "Plant deleted successfully."
            });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new
            {
                message = "Plant not found."
            });
        }
    }

    private int GetUserId()
    {
        var userIdValue =
            User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub");

        if (!int.TryParse(userIdValue, out var userId))
            throw new UnauthorizedAccessException();

        return userId;
    }
}