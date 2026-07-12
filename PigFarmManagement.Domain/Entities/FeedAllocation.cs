using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Common;

namespace PigFarmManagement.Domain.Entities
{
    public class FeedAllocation:BaseEntity
    {
        public Guid BatchId { get; set; }
        public Batch? Batch { get; set; }
        public Guid FeedTypeId { get; set; }
        public DateTime AllocationDate { get; set; }
        public decimal QuantityIssuedKg { get; set; }
        public string RecordedBy { get; set; } = string.Empty;

    }
}