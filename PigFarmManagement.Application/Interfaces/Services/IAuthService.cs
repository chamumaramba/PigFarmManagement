using System.Threading.Tasks;
using PigFarmManagement.Application.DTOs.Auth;

namespace PigFarmManagement.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<TokenResponse?> RegisterAsync(RegisterRequest request);
        Task<TokenResponse?> LoginAsync(LoginRequest request);
        Task<TokenResponse?> RefreshTokenAsync(RefreshTokenRequest request);
        Task<bool> RevokeTokenAsync(string refreshToken);
    }
}