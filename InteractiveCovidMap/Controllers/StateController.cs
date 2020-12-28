using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidTracking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CovidTracking.Controllers
{
    [Route("v1/state")]
    public class StateController : Controller
    {
        private readonly ILogger<StateController> _logger;
        private readonly IStateService _stateService;

        public StateController(ILogger<StateController> logger,IStateService stateService)
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

        [HttpGet("GetStatesCodeName")]
        public async Task<IActionResult> GetStatesCodeName()
        {
            var statesCodeName = await _stateService.GetStatesCodeNameAsync();
            if (statesCodeName == null)
            {
                return NotFound();
            }

            return Ok(statesCodeName);
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
