using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidTracking.Common
{
    public static class Constants
    {
        public static class CovidTracking
        {
            public const string BaseCovidTrackingUri = "https://api.covidtracking.com/v1/";
            public const string Status = "status.json";

            public static class State
            {
                public const string States = "states/";
                public const string CurrentStates = "current.json";
            }

            public static class County
            {
                public const string CountiesData = "https://usafactsstatic.blob.core.windows.net/public/data/covid-19/covid_deaths_usafacts.csv";
            }
        }

        public static class Geo
        {
            public const string StatesCodeName = "https://s3-us-west-2.amazonaws.com/vida-public/geo/us-state-names.tsv";
            public const string CountiesGeo = "https://cdn.jsdelivr.net/npm/us-atlas@3/counties-10m.json";
        }

        public static class Cache
        {
            public const string StateCodeNameCacheKey = "state-code-name-cache";
        }
    }
}
