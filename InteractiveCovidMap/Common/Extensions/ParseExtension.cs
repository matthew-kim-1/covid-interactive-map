using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracking.Common.Extensions
{
    public static class ParseExtension
    {
        public static int ParseStringToInt(this string source)
        {
            return int.TryParse(source, out int result) ? result : 0;
        }

        public static DateTime ParseStringToDateTime(this string source)
        {
            return DateTime.TryParse(source, out DateTime result) ? result : DateTime.MinValue;
        }
    }
}
