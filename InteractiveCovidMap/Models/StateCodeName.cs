using System.ComponentModel.DataAnnotations;

namespace CovidTracking.Models
{
    public class StateCodeName
    {
        [Key]
        public int StateCodeNameId { get; set; }

        public int Fips { get; set; }

        public string PostalCode { get; set; }

        public string StateName { get; set; }
    }
}