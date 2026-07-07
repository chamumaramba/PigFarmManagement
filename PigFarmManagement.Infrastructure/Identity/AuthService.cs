using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PigFarmManagement.Application.DTOs.Auth;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Infrastructure.Data;

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

        public async Task SaveRefreshTokenAsync(string userId, string refreshToken)
        {
            await _pigFarmDbContext.RefreshTokens.AddAsync(new RefreshToken
            {
                UserId = userId,
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            });

            await _pigFarmDbContext.SaveChangesAsync();
        }

        public async Task<TokenResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(request.Email);
            }

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return null;
            }

            var accessToken = await GenerateJwtTokenAsync(user);
            var refreshToken = Guid.NewGuid().ToString("N");
            var expiresAt = DateTime.UtcNow.AddMinutes(GetJwtDurationInMinutes());

            await SaveRefreshTokenAsync(user.Id, refreshToken);

            return new TokenResponse(accessToken, refreshToken, expiresAt);
        }

        public Task<TokenResponse?> RefreshTokenAsync(RefreshTokenRequest request)
        {
            return Task.FromResult<TokenResponse?>(null);
        }

        public Task<bool> RevokeTokenAsync(string token)
        {
            return Task.FromResult(false);
        }

        public Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            return Task.FromResult(new RegisterResponse(false, new[] { "Registration is not implemented yet." }));
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
    }
}