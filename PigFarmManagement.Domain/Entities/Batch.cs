using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Common;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Domain.Entities
{
    public class Batch: FarmEntity
    {
        public string BatchCode { get; set; } = string.Empty;
        public DateTime ConceptionDate  { get; set;}
        public DateTime StartDate { get; set; }
        public BatchStatus Status { get; set; }

        public ICollection<Animal> Animals { get; set; } = new List<Animal>();
        public ICollection<FeedAllocation> FeedAllocations { get; set; } = new List<FeedAllocation>();
        public ICollection<VaccinationSchedule> VaccinationSchedules { get; set; } = new List<VaccinationSchedule>();
        public ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();
        public ICollection<WeightRecord> WeightRecords { get; set; } = new List<WeightRecord>();
    }
}