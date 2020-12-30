using AutoMapper;
using CovidTracking.BusinessLayer.Interfaces;
using CovidTracking.HelperModels;
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

namespace CovidTracking.BusinessLayer.Services
{
    public class StateService : IStateService
    {
        private readonly ILogger<StateService> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICacheService _cacheService;
        private readonly CovidTrackingContext _context;
        private readonly string CovidTrackingStateUri = $"{Common.Constants.CovidTracking.BaseCovidTrackingUri}{Common.Constants.CovidTracking.State.States}";

        public StateService(ILogger<StateService> logger, IMapper mapper, IHttpClientFactory httpClientFactory, ICacheService cacheService, CovidTrackingContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _cacheService = cacheService;
            _context = context;
        }

        // Gets CurrentState data from database
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

        // Gets CurrentState data from third-parth API
        public async Task<IEnumerable<CurrentStateJson>> GetCovidTrackingCurrentStatesAsync()
        {
            IEnumerable<CurrentStateJson> currentStates = null;

            try
            {
                var client = _httpClientFactory.CreateClient();

                var response = await client.GetAsync($"{CovidTrackingStateUri}{Common.Constants.CovidTracking.State.CurrentStates}");
                if (response.IsSuccessStatusCode)
                {
                    currentStates = JsonConvert.DeserializeObject<IEnumerable<CurrentStateJson>>(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return currentStates;
        }

        public async Task<IEnumerable<CurrentStateCovidMap>> GetCurrentStateCovidMapsAsync()
        {
            IEnumerable<CurrentStateCovidMap> currentStateCovidMaps = null;

            try
            {
                currentStateCovidMaps = await _context.CurrentStates.Select(x => new CurrentStateCovidMap
                {
                    Fips = x.Fips,
                    StateName = x.StateName,
                    Positive = x.Positive,
                    PositiveIncrease = x.PositiveIncrease,
                    Death = x.Death,
                    DeathIncrease = x.DeathIncrease,
                    HospitalizedCumulative = x.HospitalizedCumulative,
                    HospitalizedCurrently = x.HospitalizedCurrently,
                    InIcuCumulative = x.InIcuCumulative,
                    InIcuCurrently = x.InIcuCurrently
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return currentStateCovidMaps;
        }

        public async Task<IEnumerable<StateCodeName>> GetStateCodeNamesAsync()
        {
            IEnumerable<StateCodeName> stateCodeNames = null;

            try
            {
                stateCodeNames = await _context.StateCodeNames.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return stateCodeNames;
        }

        public async Task<IEnumerable<StateCodeNameCsv>> GetStateCodeNamesFromCsvAsync()
        {
            IEnumerable<StateCodeNameCsv> stateCodeNames = null;

            try
            {
                var client = _httpClientFactory.CreateClient();

                var response = await client.GetAsync($"{Geo.StatesCodeName}");
                if (response.IsSuccessStatusCode)
                {
                    var stateCodeNameList = new List<StateCodeNameCsv>();

                    using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                    {
                        var engine = new FileHelperEngine<StateCodeNameCsv>();
                        stateCodeNameList.AddRange(engine.ReadStream(reader));
                    }

                    stateCodeNames = stateCodeNameList;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return stateCodeNames;
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

                var currentStates = (await _context.CurrentStates.ToListAsync()).ToDictionary(x => x.Fips);
                updatedCount = 0;

                if (currentStates.Count > 0)
                {
                    var updateCurrentStates = new List<CurrentState>();
                    var addCurrentStates = new List<CurrentState>();

                    foreach (var newCurrentState in newCurrentStates)
                    {
                        if (currentStates.TryGetValue(newCurrentState.Fips, out CurrentState currentState))
                        {
                            var isSuccess1 = DateTime.TryParse(newCurrentState.LastUpdateEt, out DateTime newLastUpdatedDate);
                            var isSuccess2 = DateTime.TryParse(currentState.LastUpdateEt, out DateTime oldLastUpdatedDate);

                            if (isSuccess1 && isSuccess2 && newLastUpdatedDate > oldLastUpdatedDate)
                            {
                                _mapper.Map(newCurrentState, currentState);
                                currentState.UpdatedDate = DateTime.Now;

                                updateCurrentStates.Add(currentState);
                            }
                        }
                        else
                        {
                            var addCurrentState = _mapper.Map<CurrentState>(newCurrentState);
                            addCurrentStates.Add(addCurrentState);
                        }
                    }

                    if (addCurrentStates.Count > 0) await _context.AddRangeAsync(addCurrentStates);
                    if (updateCurrentStates.Count > 0) _context.CurrentStates.UpdateRange(updateCurrentStates);
                    if (addCurrentStates.Count > 0 || updateCurrentStates.Count > 0) updatedCount = await _context.SaveChangesAsync();
                }
                // Will only hit on the first time a table is created since the table will always have data after 1st run.
                else
                {
                    var addCurrentStates = await MatchCurrentStateToStateCodeName(_mapper.Map<List<CurrentState>>(newCurrentStates));
                    if (addCurrentStates.Count > 0)
                    {
                        await _context.CurrentStates.AddRangeAsync(addCurrentStates);
                        updatedCount = await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return updatedCount;
        }

        // Checks if Covid Tracking API is online
        public async Task<bool?> CheckCovidTrackingStatusAsync()
        {
            bool? isLive = null;

            try
            {
                var client = _httpClientFactory.CreateClient();

                var response = await client.GetAsync($"{Common.Constants.CovidTracking.BaseCovidTrackingUri}{Common.Constants.CovidTracking.Status}");
                if (response.IsSuccessStatusCode)
                {
                    isLive = JsonConvert.DeserializeObject<CovidTrackingStatus>(await response.Content.ReadAsStringAsync()).Production;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return isLive;
        }

        #region private methods

        private async Task<List<CurrentState>> MatchCurrentStateToStateCodeName(List<CurrentState> currentStates)
        {
            if (currentStates != null && currentStates.Count > 0)
            {
                var stateCodeNames = await _cacheService.GetStateCodeNamesAsync();

                foreach (var currentState in currentStates)
                {
                    if (stateCodeNames.TryGetValue(currentState.Fips, out StateCodeName stateCodeName))
                    {
                        currentState.StateName = stateCodeName.StateName;
                    }
                }
            }

            return currentStates;
        }

        #endregion private methods
    }
}