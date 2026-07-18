using System;
using System.Collections.Generic;
using System.Linq;
using PigFarmManagement.Domain.Entities;
using static PigFarmManagement.Application.DTOs.Pen.PenModel;

namespace PigFarmManagement.Application.Mappings
{
    public static class PenMapper
    {
        public static PenResponse ToResponse(Pen pen)
        {
           return new PenResponse(
                pen.Id,
                pen.BuildingId,
                pen.Name,
                pen.Capacity,
                pen.Type,
                pen.IsDeleted,
                pen.Batches.Count,
                pen.CreatedAt
            );
        }


        public static IReadOnlyList<PenSummaryResponse> ToResponseList(IEnumerable<Pen> pens)
        {
            return pens.Select(p => new PenSummaryResponse(
                p.Id,
                p.Name,
                p.Type,
                p.Capacity,
                p.Batches.Count
            )).ToList();
        }
    }
}