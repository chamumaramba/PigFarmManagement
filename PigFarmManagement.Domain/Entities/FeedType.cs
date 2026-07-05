using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Domain.Entities
{
    public class FeedType
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public FeedCategory Category { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}