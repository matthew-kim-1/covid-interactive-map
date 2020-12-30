using FileHelpers;

namespace CovidTracking.Models
{
    [IgnoreFirst]
    [IgnoreEmptyLines]
    [DelimitedRecord("\t")]
    public class StateCodeNameCsv
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}