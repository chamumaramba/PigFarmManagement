using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Common;

namespace PigFarmManagement.Domain.Entities
{
    public class WeightRecord:FarmEntity
    {
        public Guid BatchId { get; set; }
        public Batch Batch { get; set; }
        public DateTime RecordDate { get; set; }
        public decimal AverageWeightKg { get; set; }
        public decimal TotalWeightKg { get; set; }
        public int HeadCount { get; set; }
    }
}