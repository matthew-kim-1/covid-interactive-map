using System.Text.Json.Serialization;

namespace CovidTracking.HelperModels
{
    public class CurrentStateJson
    {
        [JsonPropertyName("date")]
        public int Date { get; set; }

        [JsonPropertyName("state")]
        public string PostalCode { get; set; }

        [JsonPropertyName("positive")]
        public int? Positive { get; set; }

        [JsonPropertyName("probableCases")]
        public int? ProbableCases { get; set; }

        [JsonPropertyName("negative")]
        public int? Negative { get; set; }

        [JsonPropertyName("pending")]
        public int? Pending { get; set; }

        [JsonPropertyName("totalTestResultsSource")]
        public string TotalTestResultsSource { get; set; }

        [JsonPropertyName("totalTestResults")]
        public int? TotalTestResults { get; set; }

        [JsonPropertyName("hospitalizedCurrently")]
        public int? HospitalizedCurrently { get; set; }

        [JsonPropertyName("hospitalizedCumulative")]
        public int? HospitalizedCumulative { get; set; }

        [JsonPropertyName("inIcuCurrently")]
        public int? InIcuCurrently { get; set; }

        [JsonPropertyName("inIcuCumulative")]
        public int? InIcuCumulative { get; set; }

        [JsonPropertyName("onVentilatorCurrently")]
        public int? OnVentilatorCurrently { get; set; }

        [JsonPropertyName("onVentilatorCumulative")]
        public int? OnVentilatorCumulative { get; set; }

        [JsonPropertyName("recovered")]
        public int? Recovered { get; set; }

        [JsonPropertyName("dataQualityGrade")]
        public string DataQualityGrade { get; set; }

        [JsonPropertyName("lastUpdateEt")]
        public string LastUpdateEt { get; set; }

        [JsonPropertyName("death")]
        public int? Death { get; set; }

        [JsonPropertyName("totalTestsViral")]
        public int? TotalTestsViral { get; set; }

        [JsonPropertyName("positiveTestsViral")]
        public int? PositiveTestsViral { get; set; }

        [JsonPropertyName("negativeTestsViral")]
        public int? NegativeTestsViral { get; set; }

        [JsonPropertyName("positiveCasesViral")]
        public int? PositiveCasesViral { get; set; }

        [JsonPropertyName("deathConfirmed")]
        public int? DeathConfirmed { get; set; }

        [JsonPropertyName("deathProbable")]
        public int? DeathProbable { get; set; }

        [JsonPropertyName("totalTestEncountersViral")]
        public int? TotalTestEncountersViral { get; set; }

        [JsonPropertyName("totalTestsPeopleViral")]
        public int? TotalTestsPeopleViral { get; set; }

        [JsonPropertyName("totalTestsAntibody")]
        public int? TotalTestsAntibody { get; set; }

        [JsonPropertyName("positiveTestsAntibody")]
        public int? PositiveTestsAntibody { get; set; }

        [JsonPropertyName("negativeTestsAntibody")]
        public int? NegativeTestsAntibody { get; set; }

        [JsonPropertyName("totalTestsPeopleAntibody")]
        public int? TotalTestsPeopleAntibody { get; set; }

        [JsonPropertyName("positiveTestsPeopleAntibody")]
        public int? PositiveTestsPeopleAntibody { get; set; }

        [JsonPropertyName("negativeTestsPeopleAntibody")]
        public int? NegativeTestsPeopleAntibody { get; set; }

        [JsonPropertyName("totalTestsPeopleAntigen")]
        public int? TotalTestsPeopleAntigen { get; set; }

        [JsonPropertyName("positiveTestsPeopleAntigen")]
        public int? PositiveTestsPeopleAntigen { get; set; }

        [JsonPropertyName("totalTestsAntigen")]
        public int? TotalTestsAntigen { get; set; }

        [JsonPropertyName("positiveTestsAntigen")]
        public int? PositiveTestsAntigen { get; set; }

        [JsonPropertyName("fips")]
        public int Fips { get; set; }

        [JsonPropertyName("positiveIncrease")]
        public int? PositiveIncrease { get; set; }

        [JsonPropertyName("totalTestResultsIncrease")]
        public int TotalTestResultsIncrease { get; set; }

        [JsonPropertyName("deathIncrease")]
        public int DeathIncrease { get; set; }

        [JsonPropertyName("hospitalizedIncrease")]
        public int? HospitalizedIncrease { get; set; }
    }
}