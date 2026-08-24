using System;
using System.Collections.Generic;
using PigFarmManagement.Domain.Common;

namespace PigFarmManagement.Domain.Entities
{
    public class Litter : FarmEntity
    {
        public string LitterCode { get; set; } = string.Empty;

        public Guid FarrowingRecordId { get; set; }
        public FarrowingRecord FarrowingRecord { get; set; } = null!;

        public ICollection<Animal> Animals { get; set; } = new List<Animal>();
    }
}