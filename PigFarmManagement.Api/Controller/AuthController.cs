using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _authService.LoginAsync(request);
            return user is null ? Unauthorized() : Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {

            var result = await _authService.RegisterAsync(request);

            if (!result.Succeeded)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(result.Errors, "Registration failed."));
            }

            return Ok(ApiResponse<object>.SuccessResponse(null, "Registration successful."));
        }

       /*  public async Task<IActionResult> ChangePassword()
        {
            var user = await _authService.;

        } */
    }
}