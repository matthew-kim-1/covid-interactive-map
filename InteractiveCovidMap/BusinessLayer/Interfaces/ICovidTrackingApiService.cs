using CovidTracking.HelperModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CovidTracking.BusinessLayer.Interfaces
{
    public interface ICovidTrackingApiService
    {
        Task<IEnumerable<CurrentStateJson>> GetCovidTrackingCurrentStatesAsync();

        Task<bool> CheckCovidTrackingStatusAsync();
    }
}