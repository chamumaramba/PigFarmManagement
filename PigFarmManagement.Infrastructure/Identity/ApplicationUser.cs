using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PigFarmManagement.Domain.Entities;

namespace PigFarmManagement.Infrastructure.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set;}
        public string? LastName { get; set;}
        public string? Position { get; set; }
        public string? EmployeeId { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid FarmId { get; set; }
        public Farm? Farm{ get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;
        public string? FullName => $"{FirstName} {MiddleName} {LastName}";

    }
}