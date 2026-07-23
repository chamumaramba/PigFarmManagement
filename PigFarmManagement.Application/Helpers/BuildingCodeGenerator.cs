using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigFarmManagement.Application.Helpers
{
    public static class BuildingCodeGenerator
    {
        public static string Generate(string farmCode, int sequence )
        {
            return $"{farmCode}-B{sequence:D2}";
        }
    }
}