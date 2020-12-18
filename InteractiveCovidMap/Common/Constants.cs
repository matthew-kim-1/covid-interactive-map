using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSServerless2.Common
{
    public static class Constants
    {
        public static class CovidTracking 
        {
            public static readonly string BaseCovidTrackingUri = "https://api.covidtracking.com/v1/";
            public static readonly string State = "states/";
            public static readonly string CurrentStates = "current.json";
            public static readonly string Status = "status.json";
        }
    }
}
