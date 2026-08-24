using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;

namespace PigFarmManagement.Application.Helpers
{
    public class LitterCodeGenerator
    {
         public static string Generate(
            string farmCode,
            DateTime startedDate,
            int sequence)
        {
            return $"{farmCode}-{startedDate:yyMMdd}-B{sequence:D3}";
        }
    }
}