using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigFarmManagement.Domain.Enums
{
    public enum BuildingStatus
    {
        InUse = 1, // currently housing animals
        UnderRenovation = 2, // temporarily unavailable due to renovations
        UnderMaintanance = 3, // minor maintanance, not available for normal use e.g Cleaning
        Retired = 4, // no longer in use kept for historical recods
        Demolished = 5 // building removed from the farm

    }
}