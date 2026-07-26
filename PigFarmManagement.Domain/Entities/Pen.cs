
using PigFarmManagement.Domain.Common;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Domain.Entities
{
    public class Pen:FarmEntity
    {
        public string PenCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Guid BuildingId { get; set; }
        public Building Building { get; set; } = new Building();
        public int Capacity { get; set; }
        public PenType Type { get; set; }
        public ICollection<Batch> Batches { get; set; } = new List<Batch>();
    }
}