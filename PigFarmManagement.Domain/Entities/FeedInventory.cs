using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Common;

namespace PigFarmManagement.Domain.Entities
{
    public class FeedInventory:FarmEntity
    {
        public Guid FeedTypeId { get; set; }
        public decimal QuantityInStockKg { get; set; }
        public string Unit { get; set; } = string.Empty;
    }
}