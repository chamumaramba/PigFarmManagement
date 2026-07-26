using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Entities;
using static PigFarmManagement.Application.DTOs.Batch.BatchModels;

namespace PigFarmManagement.Application.Mappings
{
    public static class BatchMapper
    {
        public static BatchResponse ToResponse(Batch batch)
        {
            return new BatchResponse(
                batch.Id,
                batch.BatchCode,
                batch.FarmId,
                batch.StartDate,
                batch.EndDate,
                batch.Status,
                batch.Animals.Count,
                batch.FeedAllocations.Count,
                batch.VaccinationSchedules.Count,
                batch.Treatments.Count,
                batch.WeightRecords.Count,
                batch.CreatedAt,
                batch.UpdatedAt
            );
        }

        public static IReadOnlyList<BatchResponse> ToResponseList(IEnumerable<Batch> batches)
        {
            return batches
            .Select(ToResponse)
            .ToList();
        }

        public static BatchSummaryResponse ToSummaryResponse(Batch batch)
        {
            return new BatchSummaryResponse(
                batch.Id,
                batch.BatchCode,
                batch.Status,
                batch.Animals.Count,
                batch.StartDate,
                batch.EndDate
            );
        }

        public static IReadOnlyList<BatchSummaryResponse> ToSummaryResponseList(IEnumerable<Batch> batches)
        {
            return batches
                .Select(ToSummaryResponse)
                .ToList();
        }
    }
}