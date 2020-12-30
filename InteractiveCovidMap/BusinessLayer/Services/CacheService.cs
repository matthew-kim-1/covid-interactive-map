using CovidTracking.BusinessLayer.Interfaces;
using CovidTracking.Common;
using CovidTracking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidTracking.BusinessLayer.Services
{
    public class CacheService : ICacheService
    {
        private readonly ILogger<CacheService> _logger;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;
        private readonly CovidTrackingContext _context;

        public CacheService(ILogger<CacheService> logger, IMemoryCache cache, IConfiguration configuration ,CovidTrackingContext context)
        {
            _logger = logger;
            _cache = cache;
            _configuration = configuration;
            _context = context;
        }

        public async Task<Dictionary<int, StateCodeName>> GetStateCodeNamesAsync()
        {
            var stateCodeNames = new Dictionary<int, StateCodeName>();

            try
            {
                _cache.TryGetValue(Constants.Cache.StateCodeNameCacheKey, out stateCodeNames);

                if (stateCodeNames == null || stateCodeNames.Count == 0)
                {
                    stateCodeNames = (await _context.StateCodeNames.ToListAsync()).ToDictionary(x => x.Fips);

                    var cacheExpirationTime = _configuration.GetValue<int>("CacheExpirationTime");
                    _cache.Set(Constants.Cache.StateCodeNameCacheKey, stateCodeNames, TimeSpan.FromMinutes(cacheExpirationTime));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return stateCodeNames;
        }
    }
}