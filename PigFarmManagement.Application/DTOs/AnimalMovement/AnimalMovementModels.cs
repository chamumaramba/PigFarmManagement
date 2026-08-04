namespace PigFarmManagement.Application.DTOs.AnimalMovement
{
    public static class AnimalMovementModels
    {
        public record MoveAnimalRequest(
            Guid AnimalId,
            Guid FromPenId,
            Guid ToPenId,
            DateTime MovementDate,
            string Reason,
            string Notes
        );

        public record AnimalMovementResponse(
            Guid Id,
            Guid AnimalId,
            string AnimalTagNumber,
            Guid FromPenId,
            Guid ToPenId,
            DateTime MovementDate,
            string Reason,
            string Notes,
            DateTime CreatedAt,
            DateTime? UpdatedAt
        );

        public record AnimalMovementSummary(
            Guid Id,
            Guid AnimalId,
            Guid FromPenId,
            Guid ToPenId,
            DateTime MovementDate,
            string Reason
        );
    }
}
