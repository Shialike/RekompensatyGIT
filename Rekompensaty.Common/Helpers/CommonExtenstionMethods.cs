
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekompensaty.Common.Helpers
{
    public static class CommonExtenstionMethods
    {
        public static DateTime GetSeasonStartDate(this DateTime now)
        {
            var seasonStart = new DateTime(DateTime.Now.Year, 4, 1);
            if (now >= seasonStart)
            {
                return seasonStart;
            }
            else
            {
                return seasonStart.AddYears(-1);
            }
        }

        public static DateTime GetSeasonEndDate(this DateTime now)
        {
            var seasonStart = new DateTime(DateTime.Now.Year, 4, 1);
            if (DateTime.Now >= seasonStart)
            {
                return new DateTime(seasonStart.AddYears(1).Year, 3, 31);
            }
            else
            {
                return new DateTime(seasonStart.Year, 3, 31);
            }
        }
    }
}
