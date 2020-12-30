using CovidTracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracking.BusinessLayer.Interfaces
{
    public interface ICacheService
    {
        Task<Dictionary<int, StateCodeName>> GetStateCodeNamesAsync();
    }
}
