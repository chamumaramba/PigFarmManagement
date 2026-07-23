using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Common;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Domain.Entities
{
    public class Building: FarmEntity
    {
        public string BuildingCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int NumberOfPens { get; set; }
        public BuildingType Type { get; set; }
        public BuildingStatus Status {get; set; }
        public ICollection<Pen> Pens { get; set; } = new List<Pen>();
    }
}