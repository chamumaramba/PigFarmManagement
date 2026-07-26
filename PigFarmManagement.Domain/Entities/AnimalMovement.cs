using System;
using PigFarmManagement.Domain.Common;

namespace PigFarmManagement.Domain.Entities
{
    public class AnimalMovement: FarmEntity
    {
        public Guid AnimalId { get; set; }
        public Animal Animal { get; set; } = null!;

        public Guid FromPenId { get; set; }
        public Guid ToPenId { get; set; }

        public DateTime MovementDate { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
