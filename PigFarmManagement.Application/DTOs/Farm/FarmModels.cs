using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PigFarmManagement.Application.DTOs
{
    public class FarmModels
    {
        public record CreateFarmRequest(
            [Required, StringLength(100)] string Name,
            [Required, StringLength(200)] string Location,
            [Required, RegularExpression("^[A-Z]{3}$")] string Currency,
            [Required, StringLength(100)] string TimeZone
        );

        public record FarmResponse(
            Guid Id,
            string Name,
            string Location,
            string Currency,
            string TimeZone,
            DateTime CreatedAt,
            int BuildingCount
        );

        public record UpdateFarmRequest(
            [Required, StringLength(100)] string Name,
            [Required, StringLength(200)] string Location,
            [Required, RegularExpression("^[A-Z]{3}$")] string Currency,
            [Required, StringLength(100)] string TimeZone
        );
    }
}
