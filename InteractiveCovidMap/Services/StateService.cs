using AWSServerless2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static AWSServerless2.Common.Constants;

namespace AWSServerless2.Services
{
    public class StateService : IStateService
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string CovidTrackingStateUri = $"{CovidTracking.BaseCovidTrackingUri}{CovidTracking.State}";

        public async Task<IEnumerable<CurrentState>> GetCurrentStatesAsync()
        {
            IEnumerable<CurrentState> currentStates = null;

            try
            {
                if (await CheckCovidTrackingStatus())
                {
                    var response = await client.GetAsync($"{CovidTrackingStateUri}{CovidTracking.CurrentStates}");
                    if (response.IsSuccessStatusCode)
                    {
                        currentStates = JsonConvert.DeserializeObject<IEnumerable<CurrentState>>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return currentStates;
        }

        #region Common

        public async Task<bool> CheckCovidTrackingStatus()
        {
            bool isLive = false;

            try
            {
                var response = await client.GetAsync($"{CovidTracking.BaseCovidTrackingUri}{CovidTracking.Status}");
                if (response.IsSuccessStatusCode)
                {
                    isLive = (JsonConvert.DeserializeObject<Status>(await response.Content.ReadAsStringAsync())).production;
                }
            }
            catch (Exception ex)
            {
                isLive = false;
            }

            return isLive;
        }

        #endregion Common
    }
}