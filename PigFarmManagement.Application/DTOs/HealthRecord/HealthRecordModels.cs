using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Application.DTOs.HealthRecord
{
    public static class HealthRecordModels
    {
        public record CreateHealthRecordRequest(
            Guid AnimalId,
            Guid BatchId,
            HealthRecordType HealthRecordType,
            DateTime RecordDate,
            string? EventName,
            string? Dosage,
            string? Status,
            string? Notes,
            decimal? WeightKg,
            int HeadCount
        );

        public record UpdateHealthRecordRequest(
            HealthRecordType HealthRecordType,
            DateTime RecordDate,
            string? EventName,
            string? Dosage,
            string? Status,
            string? Notes,
            decimal? WeightKg,
            int HeadCount
        );

        public record HealthRecordResponse(
            Guid Id,
            Guid AnimalId,
            Guid BatchId,
            HealthRecordType HealthRecordType,
            DateTime RecordDate,
            string? EventName,
            string? Dosage,
            string? Status,
            string? Notes,
            decimal? WeightKg,
            int HeadCount,
            DateTime CreatedAt,
            DateTime? UpdatedAt
        );

        public record HealthRecordSummary(
            Guid Id,
            Guid AnimalId,
            HealthRecordType HealthRecordType,
            DateTime RecordDate,
            string? EventName,
            string? Status
        );
    }
}
