using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Common;

namespace PigFarmManagement.Domain.Entities
{
    public class Treatment:FarmEntity
    {
        public Guid BatchId { get; set; }
        public Batch Batch { get; set; } = null!;
        public Guid AnimalId { get; set; }
        public Animal Animal { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime TreatmentDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}