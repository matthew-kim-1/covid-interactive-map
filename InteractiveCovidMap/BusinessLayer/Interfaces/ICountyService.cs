﻿using CovidTracking.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracking.BusinessLayer.Interfaces
{
    public interface ICountyService
    {
        Task<IEnumerable<CountyDataCsv>> GetCountyDataFromCsvAsync();
    }
}
