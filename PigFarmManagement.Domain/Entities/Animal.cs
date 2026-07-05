using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Domain.Entities
{
    public class Animal
    {
        public Guid Id { get; set; }

        public string TagNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public decimal BirthWeight { get; set; }
        public decimal CurrentWeight { get; set; }

        public Gender Gender { get; set; }
        public Breed Breed { get; set; }

        // Foreign key to the Sow entity (Parent)
        public Guid? SowId { get; set; }
        public Animal? Sow { get; set; }

        // Foreign key to the Boar entity (Parent)
        public Guid? BoarId { get; set; }
        public Animal? Boar { get; set; }

        // Batch/litter
        public Guid? BatchId { get; set; }
        public Batch? Batch { get; set; }

        // Status
        public AnimalStatus Status { get; set; }
        public ProductionStage ProductionStage { get; set; }

        // Dates
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string? Notes { get; set; }

        // Navigation properties for related entities
        public ICollection<HealthRecord> HealthRecords { get; set; } = new List<HealthRecord>();
        public ICollection<AnimalMovement> AnimalMovements { get; set; } = new List<AnimalMovement>();
        public ICollection<BreedingRecord> BreedingRecords { get; set; } = new List<BreedingRecord>();
    }
}