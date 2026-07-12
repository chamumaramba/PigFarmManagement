using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Common;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Domain.Entities
{
    public class HealthRecord:BaseEntity
    {
        public Guid BatchId { get; set; }
        public Batch Batch { get; set; } = null!;

        public Guid AnimalId { get; set; }
        public Animal Animal { get; set; } = null!;

        public HealthRecordType HealthRecordType { get; set; }
        public DateTime RecordDate { get; set; }
        // Generic fields shared or usedconditionslly based on type
        public string? EventName { get; set; } // Vaccine name, Drug name
        public string? Dosage { get; set; } // Dosage for drug or vaccine
        public string? Status { get; set; } // Status for vaccine or treatment //Scheduled completed (mainly for vaccinations)
        public string? Notes { get; set; } // Additional notes or observations

        public decimal? WeightKg { get; set; } // Weight in kilograms (for weight records)
        public int HeadCount { get; set; } // Number of animals (for weight records)
    }
}