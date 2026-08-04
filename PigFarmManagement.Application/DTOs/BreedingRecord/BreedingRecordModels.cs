namespace PigFarmManagement.Application.DTOs.BreedingRecord
{
    public static class BreedingRecordModels
    {
        public record CreateBreedingRecordRequest(
            Guid AnimalId,
            Guid? SowId,
            Guid? BoarId,
            DateTime ServiceDate,
            string Notes
        );

        public record RecordPregnancyCheckRequest(
            DateTime PregnancyCheckDate,
            bool IsPregnant
        );

        public record RecordFarrowingRequest(
            DateTime FarrowingDate,
            int PigletsBornAlive,
            int PigletsBornDead,
            string Notes
        );

        public record BreedingRecordResponse(
            Guid Id,
            Guid AnimalId,
            Guid? SowId,
            Guid? BoarId,
            DateTime ServiceDate,
            DateTime? PregnancyCheckDate,
            bool? IsPregnant,
            DateTime? ExpectedFarrowingDate,
            DateTime? FarrowingDate,
            int? PigletsBornAlive,
            int? PigletsBornDead,
            string Notes,
            DateTime CreatedAt,
            DateTime? UpdatedAt
        );

        public record BreedingRecordSummary(
            Guid Id,
            Guid AnimalId,
            DateTime ServiceDate,
            bool? IsPregnant,
            DateTime? ExpectedFarrowingDate,
            DateTime? FarrowingDate
        );
    }
}
