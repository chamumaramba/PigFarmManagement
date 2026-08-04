using System.Collections.Generic;
using System.Linq;
using PigFarmManagement.Domain.Entities;
using static PigFarmManagement.Application.DTOs.AnimalModels;

namespace PigFarmManagement.Application.Mappings
{
    public static class AnimalMapper
    {
        public static AnimalResponse ToResponse(Animal animal)
        {
            return new AnimalResponse(
                animal.Id,
                animal.TagNumber,
                animal.DateOfBirth,
                animal.BirthWeight,
                animal.CurrentWeight,
                animal.Gender,
                animal.Breed,
                animal.SowId,
                animal.BoarId,
                animal.BatchId,
                animal.Status,
                animal.ProductionStage,
                animal.Notes,
                animal.CreatedAt,
                animal.UpdatedAt
            );
        }

        public static AnimalSummary ToSummary(Animal animal)
        {
            return new AnimalSummary(
                animal.Id,
                animal.TagNumber,
                animal.Gender,
                animal.Breed,
                animal.Status,
                animal.ProductionStage,
                animal.CurrentWeight
            );
        }

        public static IReadOnlyList<AnimalResponse> ToResponseList(IEnumerable<Animal> animals)
        {
            return animals
                .Select(ToResponse)
                .ToList();
        }

        public static IReadOnlyList<AnimalSummary> ToSummaryList(IEnumerable<Animal> animals)
        {
            return animals
                .Select(ToSummary)
                .ToList();
        }
    }
}