using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigFarmManagement.Domain.Entities
{
    public class WeightRecord
    {
        public Guid Id { get; set; }
        public Guid BatchId { get; set; }
        public Batch Batch { get; set; }
        public DateTime RecordDate { get; set; }
        public decimal AverageWeightKg { get; set; }
        public decimal TotalWeightKg { get; set; }
        public int HeadCount { get; set; }
    }
}