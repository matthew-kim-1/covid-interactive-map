using CovidTracking.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CovidTracking.Services
{
    public interface IStateService
    {
        Task<IEnumerable<CurrentState>> GetCurrentStatesAsync();
        
        Task<IEnumerable<CurrentState>> GetCovidTrackingCurrentStatesAsync();

        Task<IEnumerable<StateCodeName>> GetStatesCodeNameAsync();

        Task<bool?> CheckCovidTrackingStatusAsync();

        Task<int?> UpdateCurrentStatesAsync();
    }
}