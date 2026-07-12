using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Common;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Domain.Entities
{
    public class Building: BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public BuildingType Type { get; set; }
        public Farm? Farm { get; set; }
        public Guid FarmId { get; set; }
        public BuildingStatus Status {get; set; }
        public ICollection<Pen> Pens { get; set; } = new List<Pen>();
    }
}