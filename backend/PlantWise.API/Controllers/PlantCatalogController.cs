using Microsoft.AspNetCore.Mvc;
using PlantWise.Application.Interfaces.Services;

namespace PlantWise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantCatalogController : ControllerBase
{
    private readonly IPlantCatalogService _service;

    public PlantCatalogController(IPlantCatalogService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var plants = await _service.GetAllAsync();

        return Ok(plants);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var plant = await _service.GetByIdAsync(id);

        if (plant == null)
            return NotFound();

        return Ok(plant);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string q)
    {
        var plants = await _service.SearchAsync(q);

        return Ok(plants);
    }
}