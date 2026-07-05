using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigFarmManagement.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsRevoked { get; set; }
        public bool IsUsed { get; set; }
        public bool IsActive => !IsRevoked && !IsUsed && !IsExpired;
    }
}