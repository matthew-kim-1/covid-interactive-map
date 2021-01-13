using FileHelpers;

namespace CovidTracking.Models
{
    [IgnoreFirst(1)]
    [IgnoreEmptyLines]
    [DelimitedRecord("\t")]
    public class StateCodeNameCsv
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}