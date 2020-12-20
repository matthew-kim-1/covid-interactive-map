using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InteractiveCovidMap.Services;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveCovidMap.Controllers
{
    [Route("v1/state")]
    public class StateController : Controller
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService)
        {
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

        // GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
