using PigFarmManagement.Domain.Enums;
using PigFarmManagement.Application.Constants;
using PigFarmManagement.Infrastructure.Identity;

namespace PigFarmManagement.Application.Helpers;

public static class PositionRoleMapper
{
    public static IEnumerable<string> GetRoles(EmployeePosition? position)
    {
        return position switch
        {
            EmployeePosition.FarmOwner =>
                new[] { AppRoles.Admin },

            EmployeePosition.FarmManager =>
                new[] { AppRoles.FarmManager },

            EmployeePosition.Veterinarian =>
                new[] { AppRoles.Veterinarian },

            EmployeePosition.FarmWorker =>
                new[] { AppRoles.FarmWorker },

            EmployeePosition.BreedingTechnician =>
                new[] { AppRoles.FarmWorker },

            _ => Enumerable.Empty<string>()
        };
    }
}