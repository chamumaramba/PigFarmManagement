using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Common;

namespace PigFarmManagement.Domain.Entities
{
    public class Farm: BaseEntity
    {
       public string Name { get; set; } = string.Empty;
       public string FarmCode { get; set; } = string.Empty;
       public string Location { get; set; } = string.Empty;
       public int LastAnimalSequence { get; set; }
       public int LastBuildingSequence { get; set; }
       public string Currency { get; set; } = string.Empty;
       public string TimeZone { get; set; } = string.Empty;
       public ICollection<Building> Buildings { get; set; } = new List<Building>();
    }
}