using CovidTracking.BusinessLayer.Interfaces;
using CovidTracking.Data;
using CovidTracking.Models;
using FileHelpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static CovidTracking.Common.Constants;

namespace CovidTracking.BusinessLayer.Services
{
    public class StateCodeNameService : IStateCodeNameService
    {
        private readonly CovidTrackingContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public StateCodeNameService(CovidTrackingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StateCodeName>> GetAllStateCodeNamesAsync()
        {
            return await _context.StateCodeNames.ToListAsync();
        }

        public async Task<IEnumerable<StateCodeNameCsv>> GetStateCodeNamesFromCsvAsync()
        {
            var stateCodeNames = Enumerable.Empty<StateCodeNameCsv>();

            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"{Geo.StatesCodeName}");
            if (response.IsSuccessStatusCode)
            {
                var stateCodeNameList = new List<StateCodeNameCsv>();

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    var engine = new FileHelperEngine<StateCodeNameCsv>();
                    stateCodeNameList.AddRange(engine.ReadStream(reader));
                }

                stateCodeNames = stateCodeNameList;
            }

            return stateCodeNames;
        }
    }
}