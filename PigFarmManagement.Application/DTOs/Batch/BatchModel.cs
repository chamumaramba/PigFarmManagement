using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using global::PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Application.DTOs.Batch
{
    public static class BatchModels
    {
        public record CreateBatchRequest(
            string BatchCode,
            DateTime ConceptionDate,
            DateTime StartDate,
            BatchStatus Status
        );

        public record UpdateBatchRequest(
            DateTime ConceptionDate,
            DateTime StartDate,
            BatchStatus Status
        );

        public record BatchResponse(
            Guid Id,
            string BatchCode,
            Guid FarmId,
            DateTime ConceptionDate,
            DateTime StartDate,
            BatchStatus Status,
            int AnimalCount,
            int FeedAllocationCount,
            int VaccinationScheduleCount,
            int TreatmentCount,
            int WeightRecordCount,
            DateTime CreatedAt,
            DateTime? UpdatedAt
        );

        public record BatchSummaryResponse(
            Guid Id,
            string BatchCode,
            BatchStatus Status,
            int AnimalCount
        );
    }
}
