using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PigFarmManagement.Application.DTOs.Auth;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Infrastructure.Data;
using System.Security.Cryptography;

namespace PigFarmManagement.Infrastructure.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PigFarmDbContext _pigFarmDbContext;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            PigFarmDbContext pigFarmDbContext,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _pigFarmDbContext = pigFarmDbContext;
            _configuration = configuration;
        }

        private async Task SaveRefreshTokenAsync(string userId, string refreshToken)
        {
            await _pigFarmDbContext.RefreshTokens.AddAsync(new RefreshToken
            {
                UserId = userId,
                Token = HashToken(refreshToken),
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            });

            await _pigFarmDbContext.SaveChangesAsync();
        }

        public async Task<TokenResponse?> LoginAsync(LoginRequest request)
        {
            var normalizedEmail = request.Email?.Trim();
            var user = await _userManager.FindByEmailAsync(normalizedEmail);
            if (user == null && !string.IsNullOrWhiteSpace(normalizedEmail))
            {
                user = await _userManager.FindByNameAsync(normalizedEmail);
            }

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return null;
            }

            var accessToken = await GenerateJwtTokenAsync(user);
            var refreshToken = CreateRefreshToken();
            var expiresAt = DateTime.UtcNow.AddMinutes(GetJwtDurationInMinutes());

            await SaveRefreshTokenAsync(user.Id, refreshToken);

            return new TokenResponse(accessToken, refreshToken, expiresAt);
        }

        public async Task<TokenResponse?> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var storedToken = await _pigFarmDbContext.RefreshTokens
                .SingleOrDefaultAsync(token => token.Token == HashToken(request.RefreshToken));

            if (storedToken is null || !storedToken.IsActive)
            {
                return null;
            }

            var user = await _userManager.FindByIdAsync(storedToken.UserId);
            if (user is null || !user.IsActive)
            {
                return null;
            }

            storedToken.IsUsed = true;
            var newRefreshToken = CreateRefreshToken();
            await SaveRefreshTokenAsync(user.Id, newRefreshToken);

            var accessToken = await GenerateJwtTokenAsync(user);
            var expiresAt = DateTime.UtcNow.AddMinutes(GetJwtDurationInMinutes());
            return new TokenResponse(accessToken, newRefreshToken, expiresAt);
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var storedToken = await _pigFarmDbContext.RefreshTokens
                .SingleOrDefaultAsync(refreshToken => refreshToken.Token == HashToken(token));

            if (storedToken is null || storedToken.IsRevoked)
            {
                return false;
            }

            storedToken.IsRevoked = true;
            await _pigFarmDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            if (request.Password != request.ConfirmPassword)
            {
                return new RegisterResponse(false, new[] { "Password and confirmation password must match." });
            }

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new RegisterResponse(false, new[] { "Email is already registered." });
            }

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Position = request.Position,
                EmployeeId = request.EmployeeId
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return new RegisterResponse(false, result.Errors.Select(e => e.Description));
            }

            return new RegisterResponse(true, Enumerable.Empty<string>());
        }

        private static string CreateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        private static string HashToken(string token)
        {
            return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
        }

        private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT signing key is not configured.");
            var issuer = _configuration["Jwt:Issuer"] ?? "PigFarmManagement.Api";
            var audience = _configuration["Jwt:Audience"] ?? "PigFarmManagement.Client";
            var expiresAt = DateTime.UtcNow.AddMinutes(GetJwtDurationInMinutes());
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.UserName ?? string.Empty)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var keyHash = Convert.ToBase64String(
                SHA256.HashData(Encoding.UTF8.GetBytes(jwtKey))
            );

            Console.WriteLine($"[TOKEN GENERATION] JWT Key Hash: {keyHash}");

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: expiresAt,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private int GetJwtDurationInMinutes()
        {
            return int.TryParse(_configuration["Jwt:DurationInMinutes"], out var minutes) ? minutes : 60;
        }

        public async Task<IdentityResult> ChangePasswordAsync(string username, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {

                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }


            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        }
    }
}
