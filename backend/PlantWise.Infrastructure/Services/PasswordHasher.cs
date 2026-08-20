using Microsoft.AspNetCore.Identity;
using PlantWise.Application.Interfaces.Services;

namespace PlantWise.Infrastructure.Services;

public class PasswordHasherService : IPasswordHasher
{
    private readonly PasswordHasher<object> _hasher = new();

    public string Hash(string password)
    {
        return _hasher.HashPassword(new object(), password);
    }

    public bool Verify(string password, string passwordHash)
    {
        return _hasher.VerifyHashedPassword(
            new object(),
            passwordHash,
            password) == PasswordVerificationResult.Success;
    }
}