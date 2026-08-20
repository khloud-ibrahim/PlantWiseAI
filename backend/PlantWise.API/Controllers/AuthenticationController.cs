using Microsoft.AspNetCore.Mvc;
using PlantWise.Application.Features.Authentication.DTOs;
using PlantWise.Application.Features.Authentication.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using PlantWise.Application.Interfaces.Persistence;
using PlantWise.Application.Interfaces.Services;

namespace PlantWise.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
private readonly IUserRepository _userRepository;
public AuthenticationController(
    IAuthenticationService authenticationService,
    IUserRepository userRepository)
{
    _authenticationService = authenticationService;
    _userRepository = userRepository;
}

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(
        RegisterRequest request)
    {
        var result = await _authenticationService.RegisterAsync(request);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(
        LoginRequest request)
    {
        var result = await _authenticationService.LoginAsync(request);
        return Ok(result);
    }
[HttpPost("refresh-token")]
public async Task<IActionResult> RefreshToken(
    [FromBody] RefreshTokenRequest request)
{
    try
    {
        var response = await _authenticationService.RefreshTokenAsync(request);

        return Ok(response);
    }
    catch (UnauthorizedAccessException ex)
    {
        return Unauthorized(new
        {
            message = ex.Message
        });
    }
}
[HttpGet("me")]
[Authorize]
public async Task<IActionResult> Me()
{
    var userIdValue =
        User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        ?? User.FindFirst("sub")?.Value;

    if (!int.TryParse(userIdValue, out var userId))
        return Unauthorized();

    var user = await _userRepository.GetByIdAsync(userId);

    if (user is null)
        return NotFound(new
        {
            message = "User not found."
        });

    return Ok(new
    {
        message = "You are authenticated!",
        userId = user.Id,
        fullName = user.FullName,
        email = user.Email,
        experienceLevel = user.ExperienceLevel
    });
}


[HttpPost("logout")]
public async Task<IActionResult> Logout(
    [FromBody] LogoutRequest request)
{
    await _authenticationService.LogoutAsync(request);

    return Ok(new
    {
        message = "Logged out successfully."
    });
}
}