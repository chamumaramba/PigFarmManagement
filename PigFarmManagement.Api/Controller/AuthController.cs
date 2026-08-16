using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PigFarmManagement.Application.Common;
using PigFarmManagement.Application.DTOs.Auth;
using PigFarmManagement.Application.Interfaces.Services;

namespace PigFarmManagement.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var tokens = await _authService.LoginAsync(request);
            if (tokens is null)
                return Unauthorized();

            Response.Cookies.Append("pf_access", tokens.AccessToken, AccessCookieOptions(tokens.ExpiresAt));
            Response.Cookies.Append("pf_refresh", tokens.RefreshToken, RefreshCookieOptions());

            return Ok(ApiResponse<object?>.SuccessResponse(null, "Login successful."));
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {

            var result = await _authService.RegisterAsync(request);

            if (!result.Succeeded)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(result.Errors, "Registration failed."));
            }

            return Ok(ApiResponse<object?>.SuccessResponse(null, "Registration successful."));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var refreshToken = Request.Cookies["pf_refresh"];
            if (string.IsNullOrWhiteSpace(refreshToken))
                return Unauthorized();

            var tokens = await _authService.RefreshTokenAsync(refreshToken);
            if (tokens is null)
                return Unauthorized();

            Response.Cookies.Append("pf_access", tokens.AccessToken, AccessCookieOptions(tokens.ExpiresAt));
            Response.Cookies.Append("pf_refresh", tokens.RefreshToken, RefreshCookieOptions());

            return NoContent();
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke([FromBody] RefreshTokenRequest request)
        {
            await _authService.RevokeTokenAsync(request.RefreshToken);
            return NoContent();
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var result = await _authService.ChangePasswordAsync(request.Email, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(result.Errors, "Password change failed."));
            }

            return Ok(
                ApiResponse<string>.SuccessResponse(
                    "Password changed successfully."
                )
            );
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["pf_access"];

            if (!string.IsNullOrWhiteSpace(refreshToken))
                await _authService.RevokeTokenAsync(refreshToken);

            Response.Cookies.Delete("pf_access", new CookieOptions { Path = "/"});
            Response.Cookies.Delete("pf_refresh", new CookieOptions { Path = "/api/auth" });

            return NoContent();
        }

        private static CookieOptions AccessCookieOptions(DateTime expiresAt) => new()
        {
            HttpOnly = true,
            Secure = true, // HTTPS only; use false only for local HTTP development
            SameSite = SameSiteMode.Lax,
            Expires = new DateTimeOffset(expiresAt),
            Path = "/"
        };

        private static CookieOptions RefreshCookieOptions() => new()
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddDays(7),
            Path = "/api/auth"
        };
    }
}
