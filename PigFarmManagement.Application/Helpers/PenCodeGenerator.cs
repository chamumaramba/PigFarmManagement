using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigFarmManagement.Application.Helpers
{
    public static class PenCodeGenerator
    {
        public static string Generate(string buildingCode, int sequence)
        {
            return $"{buildingCode}-P{sequence:D3}";
        }
    }
}