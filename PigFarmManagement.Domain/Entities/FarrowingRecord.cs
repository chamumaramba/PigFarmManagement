using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Common;

namespace PigFarmManagement.Domain.Entities
{
    public class FarrowingRecord: FarmEntity
    {
        public Guid SowId { get; set; }
        public Animal? Sow { get; set; }

        public Guid BreedingRecordId { get; set; }
        public BreedingRecord BreedingRecord { get; set; } = null!;

        public Litter? Litter { get; set; }

        public int TotalBorn { get; set; }
        public int BornAlive { get; set; }
        public int StillBorn { get; set; }
        public DateTime FarrowingDate { get; set; }

        public string Notes {get; set; } = string.Empty;

    }
}
