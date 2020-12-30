using System;
using System.Text.Json.Serialization;

namespace CovidTracking.HelperModels
{
    public class CovidTrackingStatus
    {
        [JsonPropertyName("buildTime")]
        public DateTime BuildTime { get; set; }

        [JsonPropertyName("production")]
        public bool Production { get; set; }

        [JsonPropertyName("runNumber")]
        public int RunNumber { get; set; }
    }
}