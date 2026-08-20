using System.Security.Cryptography;
using PlantWise.Application.Features.Authentication.DTOs;
using PlantWise.Application.Interfaces.Persistence;
using PlantWise.Application.Interfaces.Services;
using PlantWise.Domain.Entities;

namespace PlantWise.Application.Features.Authentication.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public AuthenticationService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtService jwtService,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);

        if (existingUser is not null)
            throw new InvalidOperationException("Email already exists.");

        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = _passwordHasher.Hash(request.Password),
            ExperienceLevel = request.ExperienceLevel
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        var refreshToken = await CreateRefreshTokenAsync(user.Id);

        return new AuthResponse
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Token = _jwtService.GenerateToken(user),
            RefreshToken = refreshToken.Token
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null ||
            !_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        var refreshToken = await CreateRefreshTokenAsync(user.Id);

        return new AuthResponse
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Token = _jwtService.GenerateToken(user),
            RefreshToken = refreshToken.Token
        };
    }

    public async Task<AuthResponse> RefreshTokenAsync(
        RefreshTokenRequest request)
    {
        var refreshToken =
            await _refreshTokenRepository.GetByTokenAsync(
                request.RefreshToken);

        if (refreshToken is null ||
            refreshToken.IsRevoked ||
            refreshToken.ExpiryDate <= DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException(
                "Invalid or expired refresh token.");
        }

        var user =
            await _userRepository.GetByIdAsync(refreshToken.UserId);

        if (user is null)
            throw new UnauthorizedAccessException("User not found.");

        // Revoke the old refresh token
        refreshToken.IsRevoked = true;

        // Create a new refresh token
        var newRefreshToken =
            await CreateRefreshTokenAsync(user.Id);

        return new AuthResponse
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Token = _jwtService.GenerateToken(user),
            RefreshToken = newRefreshToken.Token
        };
    }

    public async Task LogoutAsync(LogoutRequest request)
    {
        var refreshToken =
            await _refreshTokenRepository.GetByTokenAsync(
                request.RefreshToken);

        if (refreshToken is null)
            return;

        refreshToken.IsRevoked = true;

        await _refreshTokenRepository.SaveChangesAsync();
    }

    private async Task<RefreshToken> CreateRefreshTokenAsync(int userId)
    {
        var refreshToken = new RefreshToken
        {
            UserId = userId,
            Token = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64)),
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        await _refreshTokenRepository.AddAsync(refreshToken);
        await _refreshTokenRepository.SaveChangesAsync();

        return refreshToken;
    }
}