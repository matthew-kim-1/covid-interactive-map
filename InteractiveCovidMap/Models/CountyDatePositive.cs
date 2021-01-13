using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracking.Models
{
    public class CountyDatePositive
    {
        [Key]
        public int CountyDatePositiveId { get; set; }
        public DateTime Date { get; set; }
        public int Positive { get; set; }
        public int CountyCodeNameId { get; set; }

        // Navigation Property
        public virtual CountyCodeName CountyCodeName { get; set; }
    }
}
