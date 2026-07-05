using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigFarmManagement.Domain.Enums
{
    public enum AnimalStatus
    {
        Alive,
        Dead,
        Sold,
        Culled,
        Quarantined,
        UnderTreatment,
        Others
    }
}