using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigFarmManagement.Domain.Entities
{
    public class FeedInventory
    {
        public Guid Id { get; set; }
        public Guid FeedTypeId { get; set; }
        public decimal QuantityInStockKg { get; set; }
        public string Unit { get; set; }
    }
}