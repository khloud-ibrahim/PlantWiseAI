using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PlantWise.Application.DependencyInjection;
using PlantWise.Infrastructure.DependencyInjection;
using PlantWise.Shared.Settings;
using System.Text;
using PlantWise.Application.Features.Users.Interfaces;
using PlantWise.Application.Features.Users.Services;
using PlantWise.Application.Interfaces.Persistence;
using PlantWise.Application.Interfaces.Services;
using PlantWise.Application.Services;
using PlantWise.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ================= CONTROLLERS =================

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// ================= CORS =================

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// ================= API =================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Enter JWT token"
        });

    options.AddSecurityRequirement(
        new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference =
                        new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type =
                                Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                Array.Empty<string>()
            }
        });
});

// ================= APPLICATION =================

builder.Services.AddApplication();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IPlantRepository, PlantRepository>();

builder.Services.AddScoped<IPlantCatalogRepository, PlantCatalogRepository>();

builder.Services.AddScoped<IPlantCatalogService, PlantCatalogService>();

// ================= INFRASTRUCTURE =================

builder.Services.AddInfrastructure(builder.Configuration);

// ================= TEST USER SERVICE =================

var testUserService = builder.Services
    .FirstOrDefault(x => x.ServiceType == typeof(IUserService));

Console.WriteLine(
    $"IUserService registered: {testUserService is not null}");

// ================= AUTHENTICATION =================

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration
            .GetSection("JwtSettings")
            .Get<JwtSettings>()
            ?? throw new InvalidOperationException(
                "JwtSettings is missing.");

        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Key)),

                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,

                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,

                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine(
                    "===== JWT AUTH FAILED =====");

                Console.WriteLine(
                    context.Exception.Message);

                Console.WriteLine(
                    "===========================");

                return Task.CompletedTask;
            }
        };
    });

// ================= AUTHORIZATION =================

builder.Services.AddAuthorization();

// ================= APP =================

var app = builder.Build();

// ================= CORS =================

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();