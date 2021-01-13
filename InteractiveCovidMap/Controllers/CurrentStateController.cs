using CovidTracking.BusinessLayer.Interfaces;
using CovidTracking.CustomException;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CovidTracking.Controllers
{
    [Route("v1/currentstate")]
    public class CurrentStateController : Controller
    {
        private readonly ICurrentStateService _currentStateService;

        public CurrentStateController(ICurrentStateService currentStateService)
        {
            _currentStateService = currentStateService;
        }

        [HttpGet("GetAllCurrentStates")]
        public async Task<IActionResult> GetAllCurrentStatesAsync()
        {
            var currentStates = await _currentStateService.GetAllCurrentStatesAsync();
            if (currentStates == null)
            {
                throw new NotFoundException("CurrentStates not found.");
            }

            return Ok(currentStates);
        }

        [HttpGet("GetCurrentStateById/{id}")]
        public async Task<IActionResult> GetCurrentStateByIdAsync(int id)
        {
            var currentState = await _currentStateService.GetCurrentStateByIdAsync(id);
            if (currentState == null)
            {
                throw new NotFoundException($"CurrentState with Id {id} not found.");
            }

            return Ok(currentState);
        }

        [HttpGet("GetCurrentStateByFips/{fips}")]
        public async Task<IActionResult> GetCurrentStateByFipsAsync(int fips)
        {
            var currentState = await _currentStateService.GetCurrentStateByFipsAsync(fips);
            if (currentState == null)
            {
                throw new NotFoundException($"CurrentState with Fips {fips} not found.");
            }

            return Ok(currentState);
        }

        [HttpGet("GetCurrentStateCovidMap")]
        public async Task<IActionResult> GetCurrentStateCovidMapsAsync()
        {
            var currentStateCovidMaps = await _currentStateService.GetCurrentStateCovidMapsAsync();
            if (currentStateCovidMaps == null)
            {
                throw new NotFoundException("CurrentStateCovidMaps not found.");
            }

            return Ok(currentStateCovidMaps);
        }

        [HttpPost("UpdateCurrentStatesByCovidTracking")]
        public async Task<IActionResult> UpdateCurrentStatesByCovidTrackingAsync()
        {
            var recordCount = await _currentStateService.UpdateCurrentStatesByCovidTrackingAsync();

            return Ok(recordCount);
        }
    }
}
