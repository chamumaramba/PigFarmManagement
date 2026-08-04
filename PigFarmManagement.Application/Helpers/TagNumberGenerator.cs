using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PigFarmManagement.Application.Helpers
{
    public class TagNumberGenerator
    {
        public static string Generate(string farmCode, DateTime dateOfBirth, int sequence)
        {
            return $"{farmCode}-{dateOfBirth:MM/YY}-P{sequence:D4}";
        }
    }
}