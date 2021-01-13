using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracking.HelperModels
{
    [IgnoreFirst(2)]
    [IgnoreEmptyLines]
    [DelimitedRecord(",")]
    public class CountyDataCsv
    {

        public int CountyFips { get; set; }

        public string CountyName { get; set; }

        public int StateFips { get; set; }

        public string StateName { get; set; }

        public List<CountyDatePositiveCsv> CountyDatePositiveCsvList { get; set; }
    }

    public class CountyDatePositiveCsv
    {
        public DateTime Date { get; set; }
        public int? Positive { get; set; }
    }
}
