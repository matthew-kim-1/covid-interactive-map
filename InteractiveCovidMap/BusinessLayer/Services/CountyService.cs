using AutoMapper;
using CovidTracking.BusinessLayer.Interfaces;
using CovidTracking.Common;
using CovidTracking.Common.Extensions;
using CovidTracking.Data;
using CovidTracking.HelperModels;
using CovidTracking.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CovidTracking.BusinessLayer.Services
{
    public class CountyService : ICountyService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CovidTrackingContext _context;

        public CountyService(ILogger<CountyService> logger, IMapper mapper, IConfiguration configuration, IHttpClientFactory httpClientFactory, CovidTrackingContext context)
        {
            _mapper = mapper;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _context = context;
        }

        public async Task<IEnumerable<CountyDataCsv>> GetCountyDataFromCsvAsync()
        {
            var countyDataList = Enumerable.Empty<CountyDataCsv>();

            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync(Constants.CovidTracking.County.CountiesData);
            if (response.IsSuccessStatusCode)
            {
                var addDbCountyCodeNameList = new List<CountyCodeName>();
                var addDbCountyDatePositiveList = new List<CountyDatePositive>();

                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {
                    var skip = 4;
                    var columnHeaders = (await reader.ReadLineAsync()).Split(",");
                    var dateHeaders = columnHeaders.Skip(skip).Take(columnHeaders.Length - skip).ToArray();

                    await reader.ReadLineAsync();

                    while (!reader.EndOfStream)
                    {
                        var countyData = (await reader.ReadLineAsync()).Split(",");
                        var countyCodeName = new CountyCodeName
                        {
                            CountyFips = countyData[0].ParseStringToInt(),
                            CountyName = countyData[1],
                            StateFips = countyData[3].ParseStringToInt(),
                            StateName = countyData[2],
                        };

                        addDbCountyCodeNameList.Add(countyCodeName);

                        var countyDatePositiveData = countyData.Skip(skip).Take(countyData.Length - skip).ToArray();
                        if (countyData.Length > 4)
                        {
                            for (int i = 0; i < countyDatePositiveData.Count(); i++)
                            {
                                var date = dateHeaders[i].ParseStringToDateTime();
                                if (date > DateTime.MinValue.Date)
                                {
                                    var countyDatePositive = new CountyDatePositive()
                                    {
                                        Date = date.Date,
                                        Positive = countyDatePositiveData[i].ParseStringToInt()
                                    };
                                    countyDatePositive.CountyCodeName = countyCodeName;

                                    if (countyDatePositive.Positive > 0)
                                    {
                                    }

                                    addDbCountyDatePositiveList.Add(countyDatePositive);
                                }
                            }
                        }
                    }
                };

                //insert CountyCodeName first then bulk insert CountyDatePositive

                if (addDbCountyCodeNameList.Count > 0)
                {
                    await _context.CountyCodeNames.AddRangeAsync(addDbCountyCodeNameList);
                    await _context.SaveChangesAsync();
                }

                if (addDbCountyDatePositiveList.Count > 0)
                {
                    var skip = 0;
                    var take = 1000;

                    while (skip < addDbCountyDatePositiveList.Count)
                    {
                        var bulk = addDbCountyDatePositiveList.Skip(skip).Take(take);
                        await _context.CountyDatePositives.AddRangeAsync(bulk);
                        skip += await _context.SaveChangesAsync();
                    }
                }
            }

            return countyDataList;
        }

        #region private methods

        private void CalculatePositivePerCounty()
        {
        }

        private async Task BulkCopy(List<CountyDatePositive> countyDatePositives)
        {
            var connectionString = _configuration.GetConnectionString("CovidTrackingConnection");
            var table = new DataTable();

            using (var adapter = new SqlDataAdapter($"SELECT TOP 0 * FROM CountyDatePositives", connectionString))
            {
                adapter.Fill(table);
            };

            for (int i = 0; i < countyDatePositives.Count; i++)
            {
                var row = table.NewRow();
                row["Date"] = countyDatePositives[i].Date;
                row["Positive"] = countyDatePositives[i].Positive;
                row["CountyCodeNameId"] = countyDatePositives[i].CountyCodeNameId;
                table.Rows.Add(row);
            }

            using (var bulk = new SqlBulkCopy(connectionString))
            {
                bulk.DestinationTableName = "CountyDatePositives";
                await bulk.WriteToServerAsync(table);
            }
        }

        #endregion private methods
    }
}