using Rekompensaty.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekompensaty.Common.Helpers
{
    public static class CommonExtenstionMethods
    {
        public static string ToString(this AnimalType type)
        {
            switch (type)
            {
                case AnimalType.Deer:
                    return "Jeleń";
                case AnimalType.FallowDeer:
                    return "Daniel";
                case AnimalType.RoeDeer:
                    return "Sarna";
                case AnimalType.Boar:
                    return "Dzik";
                case AnimalType.Fox:
                    return "Lis";
            }
            throw new NotImplementedException();
        }
    }
}
