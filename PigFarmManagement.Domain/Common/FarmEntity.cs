using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Entities;

namespace PigFarmManagement.Domain.Common
{
    public class FarmEntity: BaseEntity
    {
        public Guid FarmId { get; set; }
        public Farm Farm { get; set; } = null!;
    }
}