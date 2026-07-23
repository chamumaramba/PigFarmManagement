using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Infrastructure.Data;
using PigFarmManagement.Infrastructure.Extensions;
using PigFarmManagement.Infrastructure.Identity;
using PigFarmManagement.Infrastructure.Repository;
using Microsoft.OpenApi;
using PigFarmManagement.Application.Services;
using PigFarmManagement.Api.Infrastructure;
using System.Text;
using System.Security.Cryptography;
using FluentValidation;
using PigFarmManagement.Application.DTOs.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", document),
            new List<string>()
        }
    });

});
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "PigFarmManagement.db");
var configuredConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionString = configuredConnectionString;

if (string.IsNullOrWhiteSpace(connectionString))
{
    connectionString = $"Data Source={dbPath}";
}
else if (connectionString.StartsWith("Data Source=", StringComparison.OrdinalIgnoreCase))
{
    var dataSource = connectionString["Data Source=".Length..].Trim();
    if (!Path.IsPathRooted(dataSource))
    {
        connectionString = $"Data Source={Path.Combine(builder.Environment.ContentRootPath, dataSource)}";
    }
}

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICurrentUserServices, CurrentUserService>();

builder.Services.AddDbContext<PigFarmDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<PigFarmDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFarmService, FarmService>();
builder.Services.AddScoped<IFarmRepository, FarmRepository>();

builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("JWT signing key is not configured. Set Jwt__Key outside source control.");

if (System.Text.Encoding.UTF8.GetByteCount(jwtKey) < 32)
{
    throw new InvalidOperationException("JWT signing key must be at least 32 bytes long.");
}

var keyHash = Convert.ToBase64String(
    SHA256.HashData(Encoding.UTF8.GetBytes(jwtKey))
);

Console.WriteLine($"[JWT VALIDATION] JWT Key Hash: {keyHash}");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        var jwtIssuer = builder.Configuration["Jwt:Issuer"];
        var jwtAudience = builder.Configuration["Jwt:Audience"];

        Console.WriteLine($"[JWT Config] Issuer: {jwtIssuer}");
        Console.WriteLine($"[JWT Config] Audience: {jwtAudience}");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(jwtKey))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var authorization = context.Request.Headers.Authorization.ToString();
                Console.WriteLine($"[OnMessageReceived] Auth header: {(string.IsNullOrEmpty(authorization) ? "EMPTY" : authorization)}");

                if (string.IsNullOrWhiteSpace(authorization))
                {
                    Console.WriteLine("[OnMessageReceived] Authorization header is empty");
                    return Task.CompletedTask;
                }

                var token = authorization.Trim();

                // Remove Bearer prefix if present
                if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    token = token["Bearer ".Length..].Trim();
                }

                // Remove quotes if present
                if (token.StartsWith('"') && token.EndsWith('"') && token.Length >= 2)
                {
                    token = token[1..^1];
                }

                if (!string.IsNullOrWhiteSpace(token))
                {
                    context.Token = token;
                    Console.WriteLine($"[OnMessageReceived] Token extracted successfully (length: {token.Length})");
                }
                else
                {
                    Console.WriteLine("[OnMessageReceived] Token is empty after processing");
                }

                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"[OnAuthenticationFailed] JWT auth failed: {context.Exception?.Message}");
                Console.WriteLine($"[OnAuthenticationFailed] Exception type: {context.Exception?.GetType().Name}");
                if (context.Exception is Microsoft.IdentityModel.Tokens.SecurityTokenExpiredException)
                {
                    Console.WriteLine("[OnAuthenticationFailed] Token is expired");
                }
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                Console.WriteLine($"[OnChallenge] Challenge issued. Error: {context.Error}, ErrorDescription: {context.ErrorDescription}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("[OnTokenValidated] JWT token validated successfully!");
                var principal = context.Principal;
                if (principal != null)
                {
                    foreach (var claim in principal.Claims)
                    {
                        Console.WriteLine($"  - Claim: {claim.Type} = {claim.Value}");
                    }
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();


var app = builder.Build();

// Development identities are opt-in through user secrets/environment variables.
// Production accounts must be provisioned through an administrator workflow.
if (app.Environment.IsDevelopment())
{
    await app.SeedDevelopmentIdentityAsync(builder.Configuration);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

