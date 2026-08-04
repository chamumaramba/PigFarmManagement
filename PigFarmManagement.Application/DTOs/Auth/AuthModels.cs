using System;
using System.ComponentModel.DataAnnotations;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Application.DTOs.Auth
{

    public record LoginRequest(
        [Required] string Email,

        [Required] string Password);

    public record RegisterRequest(
        string FirstName,
        string LastName,
        Guid? FarmId,
        string Email,
        string Password,
        string ConfirmPassword,
        EmployeePosition Position,
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
    IEnumerable<string>? Errors = null
    );

    public record ChangePasswordRequest(
        string Email,
        string CurrentPassword,
        string NewPassword
    );

    public record ChangePasswordResponse(
        bool Succeeded,
        IEnumerable<string>? Errors = null
    );
}
