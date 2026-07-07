using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Application.DTOs.Auth;


namespace PigFarmManagement.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<TokenResponse?> LoginAsync(LoginRequest request);
        Task<TokenResponse?> RefreshTokenAsync(RefreshTokenRequest request);
        Task<bool> RevokeTokenAsync(string token);
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    }

}