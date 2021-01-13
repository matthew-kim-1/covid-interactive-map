using CovidTracking.BusinessLayer.Interfaces;
using CovidTracking.CustomException;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CovidTracking.Controllers
{
    [Route("v1/covidtracking")]
    public class CovidTrackingApiController : Controller
    {
        private readonly ICovidTrackingApiService _covidTrackingApiService;

        public CovidTrackingApiController(ICovidTrackingApiService covidTrackingApiService)
        {
            _covidTrackingApiService = covidTrackingApiService;
        }

        [HttpGet("CheckCovidTrackingStatus")]
        public async Task<IActionResult> CheckCovidTrackingStatus()
        {
            var status = await _covidTrackingApiService.CheckCovidTrackingStatusAsync();

            return Ok(status);
        }

        [HttpGet("GetCovidTrackingCurrentStates")]
        public async Task<IActionResult> GetCovidTrackingCurrentStatesAsync()
        {
            var covidTrackingCurrentStates = await _covidTrackingApiService.GetCovidTrackingCurrentStatesAsync();
            if (covidTrackingCurrentStates == null)
            {
                throw new NotFoundException("CovidTrackingCurrentStates not found.");
            }

            return Ok(covidTrackingCurrentStates);
        }
    }
}
