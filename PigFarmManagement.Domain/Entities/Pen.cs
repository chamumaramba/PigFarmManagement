using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Domain.Entities
{
    public class Pen
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Farm Farm { get; set; } = null!;
        public Guid BuildingId { get; set; }
        public Building Building { get; set; } = new Building();
        public int Capacity { get; set; }
        public PenType Type { get; set; }
        public ICollection<Batch> Batches { get; set; } = new List<Batch>();
    }
}