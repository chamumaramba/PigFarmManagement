using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using PigFarmManagement.Application.Interfaces.Repositories;

namespace PigFarmManagement.Application.Helpers
{
    public static class FarmCodeGenerator
    {
        public static string GenerateFarmCode(string farmName, int farmNumber)
        {
            var code = farmName
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var initials = string.Concat(code.Take(3).Select(c => char.ToUpper(c[0])));
            return $"{initials}{farmNumber:D3}";

        }
    }
}