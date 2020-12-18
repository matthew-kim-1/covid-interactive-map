using InteractiveCovidMap.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InteractiveCovidMap.Services
{
    public interface IStateService
    {
        Task<IEnumerable<CurrentState>> GetCurrentStatesAsync();

        Task<bool> CheckCovidTrackingStatus();
    }
}