using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigFarmManagement.Application.Helpers
{
    public static class BatchCodeGenerator
    {
        public static string Generate(
            string farmCode,
            DateTime conceptionDate,
            int sequence)
        {
            return $"{farmCode}-{conceptionDate:yyMMdd}-B{sequence:D3}";
        }
    }
}