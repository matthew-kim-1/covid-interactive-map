using FileHelpers;
using InteractiveCovidMap.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using static InteractiveCovidMap.Common.Constants;

namespace InteractiveCovidMap.Services
{
    public class StateService : IStateService
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string CovidTrackingStateUri = $"{CovidTracking.BaseCovidTrackingUri}{CovidTracking.State.States}";

        public async Task<IEnumerable<CurrentState>> GetCurrentStatesAsync()
        {
            IEnumerable<CurrentState> currentStates = null;

            try
            {
                var temp = await GetStatesCodeNameAsync();

                if (await CheckCovidTrackingStatus())
                {
                    var response = await client.GetAsync($"{CovidTrackingStateUri}{CovidTracking.State.CurrentStates}");
                    if (response.IsSuccessStatusCode)
                    {
                        currentStates = JsonConvert.DeserializeObject<IEnumerable<CurrentState>>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: Create logging
            }

            return currentStates;
        }

        public async Task<IEnumerable<StateCodeName>> GetStatesCodeNameAsync()
        {
            IEnumerable<StateCodeName> statesCodeName = null;

            try
            {
                var response = await client.GetAsync($"{Geo.StatesCodeName}");
                if (response.IsSuccessStatusCode)
                {
                    var stateCodeNameList = new List<StateCodeName>();

                    using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                    {
                        var engine = new FileHelperEngine<StateCodeName>();
                        stateCodeNameList.AddRange(engine.ReadStream(reader));
                    }

                    statesCodeName = stateCodeNameList;
                }
            }
            catch (Exception ex)
            {
                // TODO: Create logging
            }

            return statesCodeName;
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
                    isLive = (JsonConvert.DeserializeObject<Status>(await response.Content.ReadAsStringAsync())).Production;
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