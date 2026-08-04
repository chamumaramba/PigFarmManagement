using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Application.DTOs
{
    public class Employee
    {
        public record RegisterRequest(
        string Email,
        string Password,
        string ConfirmPassword,
        string FirstName,
        string LastName,
        EmployeePosition Position,
        string EmployeeId,
        Guid? FarmId
        );
    }
}