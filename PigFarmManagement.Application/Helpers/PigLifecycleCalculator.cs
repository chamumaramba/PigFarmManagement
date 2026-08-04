using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Application.Helpers
{
    public class PigLifecycleCalculator
    {
        public static ProductionStage Calculate(DateTime dateOfBirth)
        {
            var ageDays = (DateTime.UtcNow.Date - dateOfBirth.Date).Days;

            return ageDays switch
            {
                < 0 => throw new ArgumentException("Invalid birth date"),

                <= 28 => ProductionStage.Piglet,

                <= 70 => ProductionStage.Nursery,

                <= 120 => ProductionStage.Grower,

                <= 180 => ProductionStage.Finisher,

                _ => ProductionStage.Breeding
            };
        }
    }
}