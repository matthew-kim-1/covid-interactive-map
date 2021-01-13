using AutoMapper;
using CovidTracking.BusinessLayer.Interfaces;
using CovidTracking.Data;
using CovidTracking.HelperModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CovidTracking.BusinessLayer.Services
{
    public class CovidTrackingApiService : ICovidTrackingApiService
    {
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICacheService _cacheService;
        private readonly CovidTrackingContext _context;
        private readonly string CovidTrackingStateUri = $"{Common.Constants.CovidTracking.BaseCovidTrackingUri}{Common.Constants.CovidTracking.State.States}";

        public CovidTrackingApiService(IMapper mapper, IHttpClientFactory httpClientFactory, ICacheService cacheService, CovidTrackingContext context)
        {
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _cacheService = cacheService;
            _context = context;
        }

        // Gets CurrentStates data from CovidTracking third-party API
        public async Task<IEnumerable<CurrentStateJson>> GetCovidTrackingCurrentStatesAsync()
        {
            var currentStates = Enumerable.Empty<CurrentStateJson>();
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"{CovidTrackingStateUri}{Common.Constants.CovidTracking.State.CurrentStates}");
            if (response.IsSuccessStatusCode)
            {
                currentStates = JsonConvert.DeserializeObject<IEnumerable<CurrentStateJson>>(await response.Content.ReadAsStringAsync());
            }

            return currentStates;
        }

        // Checks if Covid Tracking API is online
        public async Task<bool> CheckCovidTrackingStatusAsync()
        {
            var isLive = false;
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"{Common.Constants.CovidTracking.BaseCovidTrackingUri}{Common.Constants.CovidTracking.Status}");
            if (response.IsSuccessStatusCode)
            {
                isLive = JsonConvert.DeserializeObject<CovidTrackingStatus>(await response.Content.ReadAsStringAsync()).Production;
            }

            return isLive;
        }
    }
}