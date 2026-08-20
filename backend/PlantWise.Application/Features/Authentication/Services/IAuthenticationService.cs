using PlantWise.Application.Features.Authentication.DTOs;

namespace PlantWise.Application.Features.Authentication.Services;

public interface IAuthenticationService
{
    
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request);
    Task LogoutAsync(LogoutRequest request);
}