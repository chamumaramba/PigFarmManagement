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
using PigFarmManagement.Application.Constants;
using System.ComponentModel.DataAnnotations;
using PigFarmManagement.Application.DTOs.Validators;
using FluentValidation;
using PigFarmManagement.Application.Helpers;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace PigFarmManagement.Infrastructure.Identity
{
    public class AuthService(
        UserManager<ApplicationUser> userManager,
        PigFarmDbContext pigFarmDbContext,
        IConfiguration configuration,
        ICurrentUserServices currentUser,
        IValidator<LoginRequest> loginValidator) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly PigFarmDbContext _pigFarmDbContext = pigFarmDbContext;
        private readonly IConfiguration _configuration = configuration;
        private readonly ICurrentUserServices _currentUser = currentUser;
        //private readonly IValidator<LoginRequestValidator> _loginValidator = loginValidator;

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
           var validationResult = await loginValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }
            var normalizedEmail = request.Email?.Trim();
            ApplicationUser? user = null;

            if (!string.IsNullOrWhiteSpace(normalizedEmail))
            {
                user = await _userManager.FindByEmailAsync(normalizedEmail!);
                if (user == null)
                {
                    user = await _userManager.FindByNameAsync(normalizedEmail);
                }
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

        public async Task<TokenResponse?> RefreshTokenAsync(string refreshToken)
        {
            var storedToken = await _pigFarmDbContext.RefreshTokens
                .SingleOrDefaultAsync(token => token.Token == HashToken(refreshToken));

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
            var currentUser = await _userManager.FindByIdAsync(_currentUser.UserId.ToString());

            if (currentUser == null)
            {
                throw new UnauthorizedAccessException("Current user not found.");
            }
            var currentRoles = await _userManager.GetRolesAsync(currentUser);

            Guid farmId;

            if (currentRoles.Contains(AppRoles.Admin))
            {
                if (request.FarmId is null)
                    throw new ValidationException(
                        "FarmId is required.");

                farmId = request.FarmId.Value;
            }
            else if (currentRoles.Contains(AppRoles.FarmManager))
            {
                if (currentUser.FarmId == null)
                {
                    throw new InvalidOperationException(
                        "Farm manager is not assigned to a farm.");
                }

                farmId = currentUser.FarmId.Value;
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
            if (request.Password != request.ConfirmPassword)
            {
                return new RegisterResponse(false, new[] { "Password and confirmation password must match." });
            }

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new RegisterResponse(false, new[] { "Email is already registered." });
            }

            // var roles = PositionRoleMapper.GetRoles(request.Position);

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Position = request.Position,
                EmployeeId = request.EmployeeId,
                FarmId = farmId, // Set this if you have a farm ID to associate with the user
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return new RegisterResponse(false, result.Errors.Select(e => e.Description));
            }

            var roles = PositionRoleMapper.GetRoles(user.Position);

            await _userManager.AddToRolesAsync(user, roles);

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
            var jwtKey = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT signing key is not configured.");

            var issuer = _configuration["Jwt:Issuer"]
                ?? "PigFarmManagement.Api";

            var audience = _configuration["Jwt:Audience"]
                    ?? "PigFarmManagement.Client";

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


            // Add FarmId only when the user belongs to a farm
            if (user.FarmId.HasValue)
            {
                claims.Add(
                    new Claim(
                        CustomClaimTypes.FarmId,
                        user.FarmId.Value.ToString()
                    )
                );
            }


            // Add roles
            claims.AddRange(
                roles.Select(role =>
                    new Claim(ClaimTypes.Role, role))
            );


            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            );

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );


            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: expiresAt,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private int GetJwtDurationInMinutes()
        {
            return int.TryParse(_configuration["Jwt:DurationInMinutes"], out var minutes) ? minutes : 60;
        }

        public async Task<ChangePasswordResponse> ChangePasswordAsync(string username, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(username);
            }

            if (user == null)
            {
                return new ChangePasswordResponse(false, new[] { "User not found." });
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                return new ChangePasswordResponse(false, result.Errors.Select(e => e.Description));
            }

            return new ChangePasswordResponse(true);
        }
    }
}
