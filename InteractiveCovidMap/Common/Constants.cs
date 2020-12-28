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
            public static readonly string BaseCovidTrackingUri = "https://api.covidtracking.com/v1/";
            public static readonly string Status = "status.json";

            public static class State
            {
                public static readonly string States = "states/";
                public static readonly string CurrentStates = "current.json";
            }

            public static class County
            {
                public static readonly string Counties = "";
            }
        }

        public static class Geo
        {
            public static readonly string StatesCodeName = "https://s3-us-west-2.amazonaws.com/vida-public/geo/us-state-names.tsv";
            public static readonly string CountiesGeo = "https://cdn.jsdelivr.net/npm/us-atlas@3/counties-10m.json";
        }
    }
}
