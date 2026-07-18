using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Application.Interfaces.Repositories;

namespace PigFarmManagement.Application.Interfaces.Repositories
{
    public interface IAnimalRepository: IRepository<Animal>
    {
        Task<Animal?> GetByTagNumberAsync(string tagNumber, CancellationToken cancellationToken = default);
    }
}