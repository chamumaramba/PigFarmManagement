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
    [AllowAnonymous]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(
                    ApiResponse<object?>.ErrorResponse(
                        null,
                        "Invalid request."
                    ));
            }
            var user = await _authService.LoginAsync(request);

             if (user == null)
            {
                return Unauthorized(
                    ApiResponse<object?>.ErrorResponse(
                        null,
                        "Invalid email or password."
                    ));
            }

            return Ok(
                ApiResponse<TokenResponse>.SuccessResponse(
                    user,
                    "Login successful."
            ));
        }

        [HttpPost("register")]
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
            var token = await _authService.RefreshTokenAsync(request);
            return token is null ? Unauthorized() : Ok(token);
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
    }
}
