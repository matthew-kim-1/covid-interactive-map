using FileHelpers;

namespace InteractiveCovidMap.Models
{
    [IgnoreFirst]
    [IgnoreEmptyLines]
    [DelimitedRecord("\t")]
    public class StateCodeName
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }
}