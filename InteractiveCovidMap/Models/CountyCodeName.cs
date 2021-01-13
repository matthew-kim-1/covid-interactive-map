using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CovidTracking.Models
{
    public class CountyCodeName
    {
        [Key]
        public int CountyCodeNameId { get; set; }

        public int CountyFips { get; set; }

        public string CountyName { get; set; }

        public int StateFips { get; set; }

        public string StateName { get; set; }

        // Navigation Property
        public virtual ICollection<CountyDatePositive> CountyDatePositives { get; set; }
    }
}