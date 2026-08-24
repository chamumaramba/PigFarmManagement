using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigFarmManagement.Application.Helpers
{
    public class CodeGenerator
    {
        public static string GetInitials(string farmName)
        {
/*             var code = farmName
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var initials = string.Concat(code.Take(3).Select(c => char.ToUpper(c[0])));
            return $"{initials}{farmNumber:D3}"; */

            return string.Concat(farmName
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Take(3)
                .Select(word => char.ToUpperInvariant(word[0])));
        }

        public static string Farm(string initials, int sequence)
        {
            return $"{initials}{sequence:D3}";
        }

        public static string Building(string farmCode, int sequence)
        {
            return $"{farmCode}-B{sequence:D2}";
        }

        public static string Litter(string farmCode, DateTime startDate, int sequence)
        {
            return $"{farmCode}-{startDate:yyMM}-L{sequence:D3}";
        }

        public static string Batch(string farmCode, DateTime StartDate, int sequence)
        {
            return $"{farmCode}-{StartDate:yyMM}-BC{sequence:D2}";
        }

        public static string AnimalTag(string farmCode, DateTime conceptionDate, int sequence)
        {
            return $"{farmCode}-{conceptionDate:yyMM}-P{sequence:D2}";
        }

        public static string Pen(string buildingCode, int sequence)
        {
            return $"{buildingCode}-P{sequence:D2}";
        }
    }
}