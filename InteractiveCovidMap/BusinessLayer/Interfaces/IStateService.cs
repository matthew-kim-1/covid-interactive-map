using CovidTracking.HelperModels;
using CovidTracking.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CovidTracking.BusinessLayer.Interfaces
{
    public interface IStateService
    {
        Task<IEnumerable<CurrentState>> GetCurrentStatesAsync();

        Task<IEnumerable<CurrentStateJson>> GetCovidTrackingCurrentStatesAsync();

        Task<IEnumerable<CurrentStateCovidMap>> GetCurrentStateCovidMapsAsync();

        Task<IEnumerable<StateCodeName>> GetStateCodeNamesAsync();

        Task<IEnumerable<StateCodeNameCsv>> GetStateCodeNamesFromCsvAsync();

        Task<bool?> CheckCovidTrackingStatusAsync();

        Task<int?> UpdateCurrentStatesAsync();
    }
}