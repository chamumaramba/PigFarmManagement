using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigFarmManagement.Domain.Entities
{
    public class Building
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Farm Farm { get; set; }
        public Guid FarmId { get; set; }
        public ICollection<Pen> Pens { get; set; }
    }
}