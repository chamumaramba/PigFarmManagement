
using PigFarmManagement.Domain.Common;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Domain.Entities
{
    public class FeedProgram:BaseEntity
    {
        public ProductionStage ProductionStage { get; set; }
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }
        public decimal MinimumWeight { get; set; }
        public decimal MaximumWeight { get; set; }
        public Guid FeedTypeId { get; set; }
        public decimal DailyFeedAmount { get; set; }
        public int FeedsPerDay { get; set; }
    }
}