using System.Text.Json.Serialization;

namespace CovidTracking.HelperModels
{
    public class CurrentStateCovidMap
    {
        [JsonPropertyName("fips")]
        public int Fips { get; set; }

        [JsonPropertyName("stateName")]
        public string StateName { get; set; }

        [JsonPropertyName("positive")]
        public int? Positive { get; set; }

        [JsonPropertyName("positiveIncrease")]
        public int? PositiveIncrease { get; set; }

        [JsonPropertyName("death")]
        public int? Death { get; set; }

        [JsonPropertyName("deathIncrease")]
        public int? DeathIncrease { get; set; }

        [JsonPropertyName("hospitalizedCumulative")]
        public int? HospitalizedCumulative { get; set; }

        [JsonPropertyName("hospitalizedCurrently")]
        public int? HospitalizedCurrently { get; set; }

        [JsonPropertyName("inIcuCumulative")]
        public int? InIcuCumulative { get; set; }

        [JsonPropertyName("inIcuCurrently")]
        public int? InIcuCurrently { get; set; }
    }
}