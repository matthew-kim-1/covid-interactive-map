using CovidTracking.Models;
using FileHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static CovidTracking.Common.Constants;

namespace CovidTracking.Services
{
    public class StateService : IStateService
    {
        private readonly ILogger<StateService> _logger;
        private readonly CovidTrackingContext _context;
        private readonly HttpClient client = new HttpClient();
        private readonly string CovidTrackingStateUri = $"{Common.Constants.CovidTracking.BaseCovidTrackingUri}{Common.Constants.CovidTracking.State.States}";

        public StateService(ILogger<StateService> logger, CovidTrackingContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<CurrentState>> GetCurrentStatesAsync()
        {
            IEnumerable<CurrentState> currentStates = null;

            try
            {
                currentStates = await _context.CurrentStates.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return currentStates;
        }

        public async Task<IEnumerable<CurrentState>> GetCovidTrackingCurrentStatesAsync()
        {
            IEnumerable<CurrentState> currentStates = null;

            try
            {
                var response = await client.GetAsync($"{CovidTrackingStateUri}{Common.Constants.CovidTracking.State.CurrentStates}");
                if (response.IsSuccessStatusCode)
                {
                    currentStates = JsonConvert.DeserializeObject<IEnumerable<CurrentState>>(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
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
                _logger.LogError(ex.ToString());
            }

            return statesCodeName;
        }

        public async Task<bool?> CheckCovidTrackingStatusAsync()
        {
            bool? isLive = null;

            try
            {
                var response = await client.GetAsync($"{Common.Constants.CovidTracking.BaseCovidTrackingUri}{Common.Constants.CovidTracking.Status}");
                if (response.IsSuccessStatusCode)
                {
                    isLive = JsonConvert.DeserializeObject<Status>(await response.Content.ReadAsStringAsync()).Production;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return isLive;
        }

        public async Task<int?> UpdateCurrentStatesAsync()
        {
            int? updatedCount = null;

            try
            {
                var isLive = await CheckCovidTrackingStatusAsync();
                if (isLive == null || !isLive.Value) return updatedCount;

                var newCurrentStates = await GetCovidTrackingCurrentStatesAsync();
                if (newCurrentStates == null || !newCurrentStates.Any()) return updatedCount;

                var currentStates = await _context.CurrentStates.ToListAsync();
                updatedCount = 0;

                if (currentStates.Count > 0)
                {
                    var updateCurrentStates = new List<CurrentState>();

                    foreach (var newCurrentState in newCurrentStates)
                    {
                        var currentState = currentStates.FirstOrDefault(x => x.Fips == newCurrentState.Fips);

                        var isSuccess1 = DateTime.TryParse(newCurrentState.LastUpdateEt, out DateTime newLastUpdatedDate);
                        var isSuccess2 = DateTime.TryParse(newCurrentState.LastUpdateEt, out DateTime oldLastUpdatedDate);

                        if (isSuccess1 && isSuccess2 && newLastUpdatedDate > oldLastUpdatedDate)
                        {
                            newCurrentState.CreatedBy = currentState.CreatedBy;
                            newCurrentState.CreatedDate = currentState.CreatedDate;
                            newCurrentState.UpdatedBy = "MATTHEW KIM";
                            newCurrentState.UpdatedDate = DateTime.Now;
                            newCurrentState.CurrentStateId = currentState.CurrentStateId;

                            updateCurrentStates.Add(newCurrentState);
                        }
                    }

                    if (updateCurrentStates.Count > 0)
                    {
                        _context.CurrentStates.UpdateRange(newCurrentStates);
                        updatedCount = await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    foreach (var newCurrentState in newCurrentStates)
                    {
                        newCurrentState.CreatedBy = "MATTHEW KIM";
                        newCurrentState.CreatedDate = DateTime.Now;
                        newCurrentState.UpdatedBy = "MATTHEW KIM";
                        newCurrentState.UpdatedDate = DateTime.Now;
                    }

                    await _context.CurrentStates.AddRangeAsync(newCurrentStates);
                    updatedCount = await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return updatedCount;
        }
    }
}