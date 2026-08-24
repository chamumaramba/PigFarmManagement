using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Common;

namespace PigFarmManagement.Domain.Entities
{
    public class VaccinationSchedule:FarmEntity
    {
        public Guid BatchId { get; set; }
        public Batch Batch { get; set; } = null!;
        public string VaccineName { get; set; } = string.Empty;
        public DateTime ScheduledDate { get; set; }
        public DateTime? AdministeredDate { get; set; }
        public string Status { get; set; } = string.Empty;

    }
}
