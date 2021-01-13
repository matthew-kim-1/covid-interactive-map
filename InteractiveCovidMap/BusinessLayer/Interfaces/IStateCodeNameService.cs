using CovidTracking.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CovidTracking.BusinessLayer.Interfaces
{
    public interface IStateCodeNameService
    {
        Task<IEnumerable<StateCodeName>> GetAllStateCodeNamesAsync();

        Task<IEnumerable<StateCodeNameCsv>> GetStateCodeNamesFromCsvAsync();
    }
}