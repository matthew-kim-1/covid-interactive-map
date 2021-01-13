using AutoMapper;
using CovidTracking.BusinessLayer.Interfaces;
using CovidTracking.CustomException;
using CovidTracking.Data;
using CovidTracking.HelperModels;
using CovidTracking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidTracking.BusinessLayer.Services
{
    public class CurrentStateService : ICurrentStateService
    {
        private readonly CovidTrackingContext _context;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly ICovidTrackingApiService _covidTrackingApiService;

        public CurrentStateService(CovidTrackingContext context, IMapper mapper, ICacheService cacheService, ICovidTrackingApiService covidTrackingApiService)
        {
            _context = context;
            _mapper = mapper;
            _cacheService = cacheService;
            _covidTrackingApiService = covidTrackingApiService;
        }

        public async Task<IEnumerable<CurrentState>> GetAllCurrentStatesAsync()
        {
            return await _context.CurrentStates.ToListAsync();
        }

        public async Task<CurrentState> GetCurrentStateByIdAsync(int id)
        {
            return await _context.CurrentStates.FirstOrDefaultAsync(x => x.CurrentStateId == id);
        }

        public async Task<CurrentState> GetCurrentStateByFipsAsync(int fips)
        {
            return await _context.CurrentStates.FirstOrDefaultAsync(x => x.Fips == fips);
        }

        public async Task UpdateCurrentStateAsync(CurrentState currentState)
        {
            _context.CurrentStates.Update(currentState);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCurrentStateRangeAsync(List<CurrentState> currentStates)
        {
            _context.CurrentStates.UpdateRange(currentStates);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCurrentStateAsync(CurrentState currentState)
        {
            _context.CurrentStates.Remove(currentState);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CurrentStateCovidMap>> GetCurrentStateCovidMapsAsync()
        {
            return await _context.CurrentStates.Select(x => new CurrentStateCovidMap
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

        // TODO: Create GetCurrentStateCovidMapsByDateAsync

        public async Task<int> UpdateCurrentStatesByCovidTrackingAsync()
        {
            int updatedCount = 0;

            var isLive = await _covidTrackingApiService.CheckCovidTrackingStatusAsync();
            if (!isLive) throw new NotFoundException("Covid Tracking API is not live.");

            var newCurrentStates = await _covidTrackingApiService.GetCovidTrackingCurrentStatesAsync();
            if (newCurrentStates == null) throw new NotFoundException("Covid Tracking CurrentStates is null."); ;

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

            return updatedCount;
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