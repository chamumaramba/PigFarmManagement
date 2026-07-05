using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigFarmManagement.Domain.Enums
{
    public enum ProductionStage
    {
        Piglet,
        Nursery,
        Grower,
        Finisher,
        Breeding,
        Gestation,
        Lactation,
        Weaning,
        Others
    }
}