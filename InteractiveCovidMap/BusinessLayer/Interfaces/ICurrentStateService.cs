using CovidTracking.HelperModels;
using CovidTracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracking.BusinessLayer.Interfaces
{
    public interface ICurrentStateService
    {
        Task<IEnumerable<CurrentState>> GetAllCurrentStatesAsync();

        Task<CurrentState> GetCurrentStateByIdAsync(int id);

        Task<CurrentState> GetCurrentStateByFipsAsync(int fips);

        Task UpdateCurrentStateAsync(CurrentState currentState);

        Task UpdateCurrentStateRangeAsync(List<CurrentState> currentStates);

        Task<int> UpdateCurrentStatesByCovidTrackingAsync();

        Task DeleteCurrentStateAsync(CurrentState currentState);

        Task<IEnumerable<CurrentStateCovidMap>> GetCurrentStateCovidMapsAsync();
    }
}
