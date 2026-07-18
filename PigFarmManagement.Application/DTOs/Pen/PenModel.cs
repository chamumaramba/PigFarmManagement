using System;
using System.ComponentModel.DataAnnotations;
using PigFarmManagement.Domain.Enums;

namespace PigFarmManagement.Application.DTOs.Pen
{
    public class PenModel
    {
        public record CreatePenRequest(
            [Required]
            Guid BuildingId,

            [Required]
            [StringLength(50, MinimumLength = 2)]
            string Name,

            [Required]
            [Range(1, 10000)]
            int Capacity,

            [Required]
            PenType PenType
        );

        public record UpdatePenRequest(
            [Required]
            [StringLength(50, MinimumLength = 2)]
            string Name,

            [Required]
            [Range(1, 10000)]
            int Capacity,

            DateTime UpdatedAt,

            [Required]
            PenType PenType
        );

        public record PenResponse(
            Guid Id,
            Guid BuildingId,
            string Name,
            int Capacity,
            PenType PenType,
            bool IsOccupied,
            int AnimalCount,
            DateTime CreatedAt
        );

        public record PenSummaryResponse(
            Guid Id,
            string Name,
            PenType PenType,
            int Capacity,
            int AnimalCount
        );
    }
}