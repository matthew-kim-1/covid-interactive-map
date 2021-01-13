using CovidTracking.BusinessLayer.Interfaces;
using CovidTracking.CustomException;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CovidTracking.Controllers
{
    [Route("v1/county")]
    public class CountyController : Controller
    {
        private readonly ICountyService _countyService;

        public CountyController(ICountyService countyService)
        {
            _countyService = countyService;
        }

        [HttpGet("GetCountyDataFromCsv")]
        public async Task<IActionResult> GetCountyDataFromCsvAsync()
        {
            var countyDataCsvs = await _countyService.GetCountyDataFromCsvAsync();
            if (countyDataCsvs == null)
            {
                throw new NotFoundException("CountyDataFromCsv not found.");
            }

            return Ok(countyDataCsvs);
        }
    }
}