using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Application.DTOs
{
    public class AnimalModels
    {
        public record CreateAnimalRequest(
            string TagNumber,
            DateTime DateOfBirth,
            decimal BirthWeight,
            decimal CurrentWeight,
            Gender Gender,
            Breed Breed,
            Guid? SowId,
            Guid? BoarId,
            Guid? BatchId,
            AnimalStatus Status,
            ProductionStage ProductionStage,
            string? Notes,
            DateTime CreatedAt

        );

        public record UpdateAnimalRequest(
            string TagNumber,
            DateTime DateOfBirth,
            decimal BirthWeight,
            decimal CurrentWeight,
            Gender Gender,
            Breed Breed,
            Guid? SowId,
            Guid? BoarId,
            Guid? BatchId,
            AnimalStatus Status,
            ProductionStage ProductionStage,
            string? Notes
        );

        public record AnimalResponse(
            Guid Id,
            string TagNumber,
            DateTime DateOfBirth,
            decimal? BirthWeight,
            decimal? CurrentWeight,
            Gender? Gender,
            Breed? Breed,
            Guid? SowId,
            Guid? BoarId,
            Guid? BatchId,
            AnimalStatus Status,
            ProductionStage ProductionStage,
            string? Notes,
            DateTime CreatedAt,
            DateTime? UpdatedAt
        );

        public record AnimalSummary(
            Guid Id,
            string TagNumber,
            Gender? Gender,
            Breed? Breed,
            AnimalStatus Status,
            ProductionStage ProductionStage,
            decimal? CurrentWeight
        );
    }
}
