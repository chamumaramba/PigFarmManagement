using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PigFarmManagement.Infrastructure.Identity
{
    public class ApplicationRole:IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName) { }

        // Add any custom fields needed (e.g. for access level auditing)
        public string? Description { get; set; }
    }
}