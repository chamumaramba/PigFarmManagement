using System;

namespace PigFarmManagement.Application.DTOs.Auth
{
    public record LoginRequest(string Email, string Password);

    public record RegisterRequest(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string ConfirmPassword,
        string? Position,
        string? EmployeeId
    );

    public record TokenResponse(
        string AccessToken,
        string RefreshToken,
        DateTime ExpiresAt
    );

    public record RefreshTokenRequest(
        string AccessToken,
        string RefreshToken
    );

    public record RegisterResponse(
    bool Succeeded,
    IEnumerable<string>? Errors = null,
    TokenResponse? TokenData = null
);

}
