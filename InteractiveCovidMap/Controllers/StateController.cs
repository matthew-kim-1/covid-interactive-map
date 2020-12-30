using CovidTracking.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CovidTracking.Controllers
{
    [Route("v1/state")]
    public class StateController : Controller
    {
        private readonly ILogger<StateController> _logger;
        private readonly IStateService _stateService;

        public StateController(ILogger<StateController> logger, IStateService stateService)
        {
            _logger = logger;
            _stateService = stateService;
        }

        // GET api/values
        [HttpGet("GetCurrentStates")]
        public async Task<IActionResult> GetCurrentStates()
        {
            var statesValues = await _stateService.GetCurrentStatesAsync();
            if (statesValues == null)
            {
                return NotFound();
            }

            return Ok(statesValues);
        }

        [HttpGet("GetCurrentStateCovidMap")]
        public async Task<IActionResult> GetCurrentStateCovidMapsAsync()
        {
            var currentStateCovidMaps = await _stateService.GetCurrentStateCovidMapsAsync();
            if (currentStateCovidMaps == null)
            {
                return NotFound();
            }

            return Ok(currentStateCovidMaps);
        }

        [HttpGet("GetStateCodeNames")]
        public async Task<IActionResult> GetStateCodeNames()
        {
            var stateCodeNames = await _stateService.GetStateCodeNamesAsync();
            if (stateCodeNames == null)
            {
                return NotFound();
            }

            return Ok(stateCodeNames);
        }

        [HttpGet("GetStateCodeNamesFromCsv")]
        public async Task<IActionResult> GetStateCodeNamesFromCsv()
        {
            var stateCodeNames = await _stateService.GetStateCodeNamesFromCsvAsync();
            if (stateCodeNames == null)
            {
                return NotFound();
            }

            return Ok(stateCodeNames);
        }

        [HttpGet("CheckCovidTrackingStatus")]
        public async Task<IActionResult> CheckCovidTrackingStatus()
        {
            var status = await _stateService.CheckCovidTrackingStatusAsync();
            if (status == null)
            {
                return NotFound();
            }

            return Ok(status);
        }

        [HttpPost("UpdateCurrentStates")]
        public async Task<IActionResult> UpdateCurrentStates()
        {
            var recordCount = await _stateService.UpdateCurrentStatesAsync();
            if (recordCount == null)
            {
                return NotFound();
            }

            return Ok(recordCount);
        }
    }
}