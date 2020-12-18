using AWSServerless2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWSServerless2.Services
{
    public interface IStateService
    {
        Task<IEnumerable<CurrentState>> GetCurrentStatesAsync();

        Task<bool> CheckCovidTrackingStatus();
    }
}