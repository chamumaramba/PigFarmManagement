using System;
using PigFarmManagement.Domain.Common;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Domain.Entities
{
    public class BreedingRecord: BaseEntity
    {
        public Guid AnimalId { get; set; }
        public Animal Animal { get; set; } = null!;

        public Guid? SowId { get; set; }
        public Animal? Sow { get; set; }

        public Guid? BoarId { get; set; }
        public Animal? Boar { get; set; }

        public DateTime ServiceDate { get; set; }
        public DateTime? PregnancyCheckDate { get; set; }
        public bool? IsPregnant { get; set; }
        public DateTime? ExpectedFarrowingDate { get; set; }
        public DateTime? FarrowingDate { get; set; }
        public int? PigletsBornAlive { get; set; }
        public int? PigletsBornDead { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
