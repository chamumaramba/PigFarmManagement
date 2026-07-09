using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigFarmManagement.Domain.Entities
{
    public class Farm
    {
       public Guid Id { get; set; }
       public string Name { get; set; }
       public string Location { get; set; }
       public string Currency { get; set; }
       public string TimeZone { get; set; }
       public DateTime CreatedAt { get; set; }
       public bool IsActive { get; set; } = true;
       public ICollection<Building> Building { get; set; }
    }
}